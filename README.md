# Quiz App - IT 08

ระบบจัดการข้อสอบ - Test No. 8

## Tech Stack
- **Backend**: C# ASP.NET Core 9 Web API + Entity Framework Core + SQLite
- **Frontend**: Vue 3 + Vite + Vue Router + Axios

## การติดตั้งและรันโปรแกรม

### Backend

```bash
cd backend/QuizApp
dotnet run --urls "http://localhost:5000"
```

API จะรันที่ `http://localhost:5000`

### Frontend

```bash
cd frontend/quiz-frontend
npm install
npm run dev
```

Frontend จะรันที่ `http://localhost:5173`

## API Endpoints

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/api/questions` | ดึงรายการข้อสอบทั้งหมด (เรียงตาม OrderNumber) |
| POST | `/api/questions` | เพิ่มข้อสอบใหม่ |
| DELETE | `/api/questions/{id}` | ลบข้อสอบและเรียง Running Number ใหม่ |

## Database Schema

ใช้ SQLite ผ่าน Entity Framework Core (สร้าง `quiz.db` อัตโนมัติ)

```
Questions
- Id          int PK (auto increment)
- QuestionText string
- Answer1      string
- Answer2      string
- Answer3      string
- Answer4      string
- OrderNumber  int (running number)
```

## หน้าจอการทำงาน

### IT 08-1 (หน้าหลัก - รายการข้อสอบ)
- กดปุ่ม "เพิ่มข้อสอบ" เพื่อไปหน้าเพิ่มข้อสอบ
- แสดงรายการข้อสอบพร้อมตัวเลือก 4 ข้อ
- กดปุ่ม "ลบ" เพื่อลบข้อสอบ และ Running Number จะถูก re-sequence

### IT 08-2 (หน้าเพิ่มข้อสอบ)
- กรอกคำถามและคำตอบ 4 ข้อ
- กด "บันทึก" เพื่อบันทึกและกลับหน้าหลัก
- กด "ยกเลิก" เพื่อกลับหน้าหลักโดยไม่บันทึก
