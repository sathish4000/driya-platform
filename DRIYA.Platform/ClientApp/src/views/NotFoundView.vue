<template>
  <div class="not-found">
    <div class="not-found-content">
      <div class="error-code">404</div>
      <h1>Page Not Found</h1>
      <p>The page you're looking for doesn't exist or has been moved.</p>
      <div class="actions">
        <button @click="goHome" class="home-button">Go Home</button>
        <button @click="goBack" class="back-button">Go Back</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter()

const goHome = () => {
  router.push('/')
}

const goBack = () => {
  router.go(-1)
}
</script>

<style scoped>
.not-found {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 60vh;
  text-align: center;
}

.not-found-content {
  max-width: 500px;
  padding: 2rem;
}

.error-code {
  font-size: 8rem;
  font-weight: bold;
  color: #e74c3c;
  margin: 0;
  line-height: 1;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.1);
}

.not-found-content h1 {
  color: #2c3e50;
  margin: 1rem 0;
  font-size: 2.5rem;
}

.not-found-content p {
  color: #7f8c8d;
  margin: 1rem 0 2rem 0;
  font-size: 1.1rem;
  line-height: 1.6;
}

.actions {
  display: flex;
  gap: 1rem;
  justify-content: center;
  flex-wrap: wrap;
}

.home-button, .back-button {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: transform 0.2s;
}

.home-button {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
}

.home-button:hover {
  transform: translateY(-2px);
}

.back-button {
  background: #f8f9fa;
  color: #6c757d;
  border: 2px solid #e9ecef;
}

.back-button:hover {
  background: #e9ecef;
  transform: translateY(-2px);
}
</style>

