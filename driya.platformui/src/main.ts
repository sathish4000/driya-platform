// src/main.ts

import { createApp } from 'vue'
import App from './App.vue'
import {
  initializeFaro,
  ErrorsInstrumentation,
  ConsoleInstrumentation,
  SessionInstrumentation,
  getWebInstrumentations,
  LogLevel
} from '@grafana/faro-web-sdk'
import { getCLS, getFID, getLCP, type Metric } from 'web-vitals';
import { type RouteLocationNormalized } from 'vue-router'
import router from './router' // Import the router configuration

// 2. Initialize Grafana Faro SDK
const faro = initializeFaro({
  url: import.meta.env.VITE_GRAFANA_COLLECTOR_URL || 'http://localhost:4318', // Grafana OpenTelemetry Collector URL
  app: {
    name: 'DRIYA-PlatformUI',
    version: import.meta.env.VITE_APP_VERSION || '0.1.0',
    environment: import.meta.env.MODE || 'development',
  },
  // Use built-in web instrumentations for common frontend events
  instrumentations: [
    ...getWebInstrumentations(),
    new ErrorsInstrumentation(),  // Capture JavaScript errors
    new ConsoleInstrumentation(), // Capture console messages
    new SessionInstrumentation()  // Track user session attributes
  ]
})

// Add validation
if (!import.meta.env.VITE_GRAFANA_COLLECTOR_URL) {
  console.warn('Grafana collector URL not set!');
  faro.api.pushLog(['Missing Grafana collector URL'], { level: LogLevel.WARN });
}

// Log a custom message when the application starts
faro.api.pushLog(['Application initialized'], { level: LogLevel.INFO })

// 3. Capture Performance Metrics & Core Web Vitals
const sendCoreWebVitals = (name: string, value: number) => {
  faro.api.pushMeasurement({
    type: 'custom',
    values: {
      [name]: value
    }
  });
};

// Use web-vitals to monitor Core Web Vitals metrics
getCLS((metric: Metric) => sendCoreWebVitals('CLS', metric.value));
getFID((metric: Metric) => sendCoreWebVitals('FID', metric.value));
getLCP((metric: Metric) => sendCoreWebVitals('LCP', metric.value));

// Optionally push navigation timing metrics
if ('performance' in window) {
  const navigationTiming = performance.getEntriesByType('navigation')[0] as PerformanceNavigationTiming
  faro.api.pushMeasurement({
    type: 'custom',
    values: {
      dns: navigationTiming.domainLookupEnd - navigationTiming.domainLookupStart,
      tcp: navigationTiming.connectEnd - navigationTiming.connectStart,
      ttfb: navigationTiming.responseStart - navigationTiming.requestStart
    }
  })
}

// 4. Track User Sessions
faro.api.setSession({
  attributes: {
    userId: 'guest',   // Replace with actual user ID if available
    username: 'guest',
    role: 'anonymous'
  }
})

// 5. Track Route Changes
router.afterEach((to: RouteLocationNormalized, from: RouteLocationNormalized) => {
  faro.api.pushEvent('routeChange', {
    from: from.fullPath,
    to: to.fullPath,
    name: to.name?.toString() || 'unnamed-route',
    params: JSON.stringify(to.params) // Capture route params safely
  })
})

// Track custom metrics
export const perf = {
  start: (name: string) => performance.mark(`${name}-start`),
  end: (name: string) => {
    performance.mark(`${name}-end`);
    performance.measure(name, `${name}-start`, `${name}-end`);
    faro.api.pushMeasurement({
      type: 'custom',
      values: {
        [name]: performance.getEntriesByName(name)[0]?.duration
      }
    });
  }
};


// 6. Create and Mount the Vue Application
const app = createApp(App)
app.use(router)
app.provide('faro', faro) // Provide Faro globally if you need to access it in components


app.config.errorHandler = (err: unknown, instance, info) => {
  if (err instanceof Error) {
    faro.api.pushError(err, {
      context: {
        component: instance?.$.type.name || 'Unknown',
        lifecycleHook: info,
        props: instance ? JSON.stringify(
                              Object.fromEntries(
                                Object.entries(instance.$props)
                                  .map(([key, val]) => [key, typeof val])
                              )
                            ) : 'none'
      }
    })
  }
}

app.mount('#app')
