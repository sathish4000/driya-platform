<template>
  <div class="dashboard">
    <div class="dashboard-header">
      <h1>Dashboard</h1>
      <button @click="logout" class="logout-button">Logout</button>
    </div>
    
    <div class="dashboard-content">
      <div class="stats-grid">
        <div class="stat-card">
          <div class="stat-icon">👥</div>
          <h3>Total Users</h3>
          <p class="stat-number">1,234</p>
          <p class="stat-label">Active users in the system</p>
        </div>
        <div class="stat-card">
          <div class="stat-icon">🔄</div>
          <h3>Active Sessions</h3>
          <p class="stat-number">567</p>
          <p class="stat-label">Current active sessions</p>
        </div>
        <div class="stat-card">
          <div class="stat-icon">✅</div>
          <h3>System Status</h3>
          <p class="stat-number status-good">Online</p>
          <p class="stat-label">All systems operational</p>
        </div>
        <div class="stat-card">
          <div class="stat-icon">📈</div>
          <h3>Performance</h3>
          <p class="stat-number">99.9%</p>
          <p class="stat-label">System uptime</p>
        </div>
      </div>
      
      <div class="recent-activity">
        <h2>Recent Activity</h2>
        <div class="activity-list">
          <div class="activity-item">
            <div class="activity-icon">👤</div>
            <div class="activity-content">
              <span class="activity-text">New user registration</span>
              <span class="activity-time">2 minutes ago</span>
            </div>
          </div>
          <div class="activity-item">
            <div class="activity-icon">💾</div>
            <div class="activity-content">
              <span class="activity-text">Database backup completed</span>
              <span class="activity-time">5 minutes ago</span>
            </div>
          </div>
          <div class="activity-item">
            <div class="activity-icon">🔧</div>
            <div class="activity-content">
              <span class="activity-text">System maintenance completed</span>
              <span class="activity-time">10 minutes ago</span>
            </div>
          </div>
          <div class="activity-item">
            <div class="activity-icon">📊</div>
            <div class="activity-content">
              <span class="activity-text">Analytics report generated</span>
              <span class="activity-time">15 minutes ago</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter()

const logout = () => {
  console.log('Logging out...')
  router.push('/login')
}
</script>

<style scoped>
.dashboard {
  min-height: 60vh;
}

.dashboard-header {
  background: white;
  padding: 1.5rem 2rem;
  border-radius: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  margin-bottom: 2rem;
}

.dashboard-header h1 {
  color: #2c3e50;
  margin: 0;
  font-size: 2rem;
}

.logout-button {
  background: #e74c3c;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  border-radius: 6px;
  cursor: pointer;
  font-weight: 600;
  transition: background-color 0.2s;
}

.logout-button:hover {
  background: #c0392b;
}

.dashboard-content {
  display: flex;
  flex-direction: column;
  gap: 2rem;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
}

.stat-card {
  background: white;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  text-align: center;
  transition: transform 0.2s;
}

.stat-card:hover {
  transform: translateY(-5px);
}

.stat-icon {
  font-size: 3rem;
  margin-bottom: 1rem;
}

.stat-card h3 {
  color: #7f8c8d;
  margin: 0 0 1rem 0;
  font-size: 1rem;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.stat-number {
  font-size: 2.5rem;
  font-weight: bold;
  color: #2c3e50;
  margin: 0 0 0.5rem 0;
}

.status-good {
  color: #27ae60;
}

.stat-label {
  color: #7f8c8d;
  margin: 0;
  font-size: 0.9rem;
}

.recent-activity {
  background: white;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.recent-activity h2 {
  color: #2c3e50;
  margin: 0 0 1.5rem 0;
  font-size: 1.5rem;
}

.activity-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.activity-item {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 1rem;
  background: #f8f9fa;
  border-radius: 8px;
  transition: background-color 0.2s;
}

.activity-item:hover {
  background: #e9ecef;
}

.activity-icon {
  font-size: 1.5rem;
  width: 40px;
  text-align: center;
}

.activity-content {
  display: flex;
  flex-direction: column;
  flex: 1;
}

.activity-text {
  color: #2c3e50;
  font-weight: 500;
}

.activity-time {
  color: #7f8c8d;
  font-size: 0.9rem;
  margin-top: 0.25rem;
}
</style>

