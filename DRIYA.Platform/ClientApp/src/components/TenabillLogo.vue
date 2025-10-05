<template>
  <div class="tenabill-logo" :style="logoStyle">
    tenabill
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

interface Props {
  size?: number
  color?: string
  align?: 'left' | 'center' | 'right'
}

const props = withDefaults(defineProps<Props>(), {
  size: 72,
  color: '#333333',
  align: 'center'
})

const logoStyle = computed(() => ({
  fontSize: `${props.size}px`,
  color: props.color,
  textAlign: props.align
}))
</script>

<style scoped>
.tenabill-logo {
  /* Font Properties */
  font-family: 'Poppins', sans-serif;
  font-weight: 700; /* Set to Bold */
  text-transform: lowercase;
  
  /* Default styling */
  line-height: 1.2;
  letter-spacing: -0.02em;
  
  /* Smooth rendering */
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
