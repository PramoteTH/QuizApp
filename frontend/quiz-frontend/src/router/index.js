import { createRouter, createWebHistory } from 'vue-router'
import IT08_1 from '../views/IT08_1.vue'
import IT08_2 from '../views/IT08_2.vue'

const routes = [
  { path: '/', component: IT08_1 },
  { path: '/add', component: IT08_2 }
]

export default createRouter({
  history: createWebHistory(),
  routes
})
