import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5000/api'
})

export default {
  getQuestions() {
    return api.get('/questions')
  },
  createQuestion(question) {
    return api.post('/questions', question)
  },
  deleteQuestion(id) {
    return api.delete(`/questions/${id}`)
  }
}
