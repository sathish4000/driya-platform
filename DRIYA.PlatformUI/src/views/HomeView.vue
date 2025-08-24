<template>
  <div class="home">
    <div class="hero">
      <h1>Welcome to DRIYA Platform</h1>
      <p>A modern full-stack application built with Vue.js and ASP.NET Core</p>
    </div>
    
    <div class="features">
      <h2>Features</h2>
      <div class="feature-grid">
        <div class="feature-card">
          <h3>🎨 Modern UI</h3>
          <p>Built with Vue.js 3 and TypeScript for a responsive, modern interface</p>
        </div>
        <div class="feature-card">
          <h3>⚡ Fast Performance</h3>
          <p>Optimized with Vite for lightning-fast development and builds</p>
        </div>
        <div class="feature-card">
          <h3>🔧 ASP.NET Core Backend</h3>
          <p>Robust API built with .NET 9 and Entity Framework</p>
        </div>
        <div class="feature-card">
          <h3>🗄️ PostgreSQL Database</h3>
          <p>Reliable data storage with Redis caching for performance</p>
        </div>
        <div class="feature-card">
          <h3>🐳 Docker Ready</h3>
          <p>Containerized deployment with Docker Compose</p>
        </div>
        <div class="feature-card">
          <h3>🔍 Visual Studio Integration</h3>
          <p>Full debugging support in Visual Studio</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// Home view component
</script>

<style scoped>
.home {
  text-align: center;
}

.hero {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 4rem 2rem;
  border-radius: 12px;
  margin-bottom: 3rem;
}

.hero h1 {
  font-size: 3rem;
  margin-bottom: 1rem;
}

.hero p {
  font-size: 1.2rem;
  opacity: 0.9;
}

.features h2 {
  font-size: 2.5rem;
  margin-bottom: 2rem;
  color: #2c3e50;
}

.feature-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 2rem;
  margin-top: 2rem;
}

.feature-card {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s;
}

.feature-card:hover {
  transform: translateY(-5px);
}

.feature-card h3 {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  color: #2c3e50;
}

.feature-card p {
  color: #666;
  line-height: 1.6;
}
</style>

