<template>
  <div class="page">
    <div class="header">IT 08-1</div>
    <div class="content">
      <button class="btn-add" @click="$router.push('/add')">เพิ่มข้อสอบ</button>

      <div v-if="loading" class="loading">กำลังโหลด...</div>

      <div v-for="question in questions" :key="question.id" class="question-block">
        <div class="question-header">
          <span class="question-text">{{ question.orderNumber }}. {{ question.questionText }}</span>
          <button class="btn-delete" @click="deleteQuestion(question.id)">ลบ</button>
        </div>
        <div class="answers">
          <label v-for="n in 4" :key="n" class="answer-option">
            <input type="radio" :name="`q${question.id}`" disabled />
            {{ getAnswer(question, n) }}
          </label>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '../services/api'

const questions = ref([])
const loading = ref(false)

async function loadQuestions() {
  loading.value = true
  try {
    const res = await api.getQuestions()
    questions.value = res.data
  } finally {
    loading.value = false
  }
}

function getAnswer(question, n) {
  return question[`answer${n}`]
}

async function deleteQuestion(id) {
  await api.deleteQuestion(id)
  await loadQuestions()
}

onMounted(loadQuestions)
</script>

<style scoped>
.page {
  min-height: 100vh;
  background: #fff;
}

.header {
  background: #2e7d32;
  color: #fff;
  text-align: center;
  padding: 10px;
  font-size: 16px;
}

.content {
  padding: 30px 40px;
}

.btn-add {
  background: #4caf50;
  color: #fff;
  border: none;
  padding: 8px 16px;
  font-size: 14px;
  border-radius: 4px;
  cursor: pointer;
  margin-bottom: 20px;
}

.btn-add:hover {
  background: #388e3c;
}

.question-block {
  margin-bottom: 30px;
}

.question-header {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 8px;
}

.question-text {
  font-size: 14px;
  font-weight: 500;
}

.btn-delete {
  background: #d32f2f;
  color: #fff;
  border: none;
  padding: 4px 12px;
  font-size: 13px;
  border-radius: 4px;
  cursor: pointer;
}

.btn-delete:hover {
  background: #b71c1c;
}

.answers {
  display: flex;
  flex-direction: column;
  gap: 6px;
  padding-left: 10px;
}

.answer-option {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 14px;
  color: #333;
}

.answer-option input[type="radio"] {
  width: 18px;
  height: 18px;
  accent-color: #666;
}

.loading {
  color: #666;
  padding: 20px 0;
}
</style>
