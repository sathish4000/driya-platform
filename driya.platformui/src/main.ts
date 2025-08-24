import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import HomeView from './views/HomeView.vue'
import LoginView from './views/LoginView.vue'
import DashboardView from './views/DashboardView.vue'
import NotFoundView from './views/NotFoundView.vue'

// Create router
const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: HomeView },
    { path: '/login', component: LoginView },
    { path: '/dashboard', component: DashboardView },
    { path: '/:pathMatch(.*)*', component: NotFoundView }
  ]
})

// Create and mount app
const app = createApp(App)
app.use(router)
app.mount('#app')
