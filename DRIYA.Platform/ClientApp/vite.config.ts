import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'path'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': resolve(__dirname, './src')
    }
  },
  server: {
    port: 3000,
    open: true,
    host: true,
    proxy: {
      '/api': {
        target: 'https://localhost:7001',
        changeOrigin: true,
        secure: false
      }
    }
  },
  build: {
    outDir: '../wwwroot',
    emptyOutDir: false,
    rollupOptions: {
      output: {
        manualChunks: undefined
      }
    }
  }
})
