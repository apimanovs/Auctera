import { createApp } from 'vue'
import { createPinia } from 'pinia'
import './style.css'
import App from './App.vue'
import router from './app/router/router'

createApp(App)
.use(createPinia())
.use(router)
.mount('#app')
