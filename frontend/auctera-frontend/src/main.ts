import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './app/router/router'

createApp(App)
.use(router)
.mount('#app')
