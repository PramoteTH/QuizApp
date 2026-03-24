<template>
  <div class="page">
    <div class="header">IT 08-2</div>
    <div class="content">
      <div class="form">
        <div class="form-row">
          <label class="form-label">คำถาม</label>
          <input v-model="form.questionText" type="text" class="form-input" />
        </div>
        <div class="form-row">
          <label class="form-label">คำตอบ 1</label>
          <input v-model="form.answer1" type="text" class="form-input" />
        </div>
        <div class="form-row">
          <label class="form-label">คำตอบ 2</label>
          <input v-model="form.answer2" type="text" class="form-input" />
        </div>
        <div class="form-row">
          <label class="form-label">คำตอบ 3</label>
          <input v-model="form.answer3" type="text" class="form-input" />
        </div>
        <div class="form-row">
          <label class="form-label">คำตอบ 4</label>
          <input v-model="form.answer4" type="text" class="form-input" />
        </div>

        <div class="form-actions">
          <button class="btn-save" @click="save">บันทึก</button>
          <button class="btn-cancel" @click="$router.push('/')">ยกเลิก</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

const router = useRouter()

const form = reactive({
  questionText: '',
  answer1: '',
  answer2: '',
  answer3: '',
  answer4: ''
})

async function save() {
  if (!form.questionText.trim()) {
    alert('กรุณากรอกคำถาม')
    return
  }
  await api.createQuestion({ ...form })
  router.push('/')
}
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
  display: flex;
  justify-content: center;
  padding: 40px 20px;
}

.form {
  width: 100%;
  max-width: 600px;
}

.form-row {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.form-label {
  width: 80px;
  font-size: 14px;
  text-align: right;
  margin-right: 16px;
  flex-shrink: 0;
}

.form-input {
  flex: 1;
  border: 1px solid #000;
  padding: 6px 8px;
  font-size: 14px;
  outline: none;
}

.form-actions {
  display: flex;
  justify-content: center;
  gap: 16px;
  margin-top: 30px;
}

.btn-save {
  background: #1565c0;
  color: #fff;
  border: none;
  padding: 8px 24px;
  font-size: 14px;
  border-radius: 4px;
  cursor: pointer;
}

.btn-save:hover {
  background: #0d47a1;
}

.btn-cancel {
  background: #d32f2f;
  color: #fff;
  border: none;
  padding: 8px 24px;
  font-size: 14px;
  border-radius: 4px;
  cursor: pointer;
}

.btn-cancel:hover {
  background: #b71c1c;
}
</style>
