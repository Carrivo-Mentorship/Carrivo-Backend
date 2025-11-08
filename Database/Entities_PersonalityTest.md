# شرح Entities - Personality Test Module

## 1. PersonalityType (نوع الشخصية)

**ده إيه؟**
- ده أنواع الشخصيات اللي ممكن الطالب يطلع واحد منها
- زي: ANALYTICAL, CREATIVE, PRACTICAL, إلخ

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف نوع الشخصية
- **استخدامه:** Primary Key

### Code (string)
- **ده إيه:** كود نوع الشخصية
- **استخدامه:** اسم مختصر unique
- **مثال:** `ANALYTICAL`, `CREATIVE`
- **ملاحظة:** لازم يكون Unique

### Name (string)
- **ده إيه:** i18n key للاسم
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `personality.analytical.name`
- **الترجمة:**
  - English: `Analytical Thinker`
  - Arabic: `المفكر التحليلي`

### Description (string)
- **ده إيه:** i18n key للوصف
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `personality.analytical.description`
- **الترجمة:**
  - English: `Logical, detail-oriented, problem solver`
  - Arabic: `منطقي، دقيق، يحل المشاكل`

### IconUrl (string?)
- **ده إيه:** رابط أيقونة الشخصية
- **استخدامه:** عشان نعرض صورة أو أيقونة
- **مثال:** `https://storage.com/icons/analytical.svg`
- **ملاحظة:** مش إجباري

### IsActive (bool)
- **ده إيه:** هل نوع الشخصية ده نشط ولا لأ
- **استخدامه:** عشان نقدر نخفي أنواع معينة
- **القيم:** `true` أو `false`

---

## 2. PersonalityTest (الاختبار)

**ده إيه؟**
- ده الاختبار نفسه اللي الطالب هيحله
- ممكن يكون فيه versions مختلفة (v1.0, v2.0)

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الاختبار

### Version (string)
- **ده إيه:** رقم الإصدار
- **استخدامه:** عشان نعرف ده إصدار كام من الاختبار
- **مثال:** `"1.0"`, `"2.0"`
- **ملاحظة:** لازم يكون Unique

### Title (string)
- **ده إيه:** i18n key للعنوان
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `test.personality.v1.title`
- **الترجمة:**
  - English: `Career Personality Assessment`
  - Arabic: `تقييم الشخصية المهنية`

### Description (string)
- **ده إيه:** i18n key للوصف
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `test.personality.v1.description`
- **الترجمة:**
  - English: `Discover your ideal career path`
  - Arabic: `اكتشف مسارك المهني المثالي`

### IsActive (bool)
- **ده إيه:** هل الاختبار ده نشط ولا لأ
- **استخدامه:** عشان نقدر نوقف اختبار قديم ونشغل واحد جديد

---

## 3. PersonalityQuestion (سؤال الاختبار)

**ده إيه؟**
- ده الأسئلة اللي في الاختبار
- كل اختبار فيه 10-15 سؤال

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف السؤال

### PersonalityTestId (int)
- **ده إيه:** رقم الاختبار اللي السؤال ده تابع له (Foreign Key)
- **العلاقة:** Many-to-One مع PersonalityTest

### Question (string)
- **ده إيه:** i18n key للسؤال
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `test.question.1.text`
- **الترجمة:**
  - English: `Do you prefer working alone or in a team?`
  - Arabic: `بتفضل تشتغل لوحدك ولا في فريق؟`

### QuestionType (QuestionType)
- **ده إيه:** نوع السؤال
- **القيم:**
  - `MultipleChoice = 1` (اختيار من متعدد)
  - `Scale = 2` (مقياس من 1 لـ 5)
  - `YesNo = 3` (نعم/لا)

### OptionsJson (string?)
- **ده إيه:** الاختيارات (JSON)
- **استخدامه:** لو السؤال Multiple Choice
- **مثال:** `["Alone", "In a team", "Both"]`
- **ملاحظة:** بنحفظه كـ JSON

### OrderIndex (int)
- **ده إيه:** ترتيب السؤال
- **استخدامه:** عشان نعرض الأسئلة بالترتيب
- **مثال:** `1`, `2`, `3`

---

## 4. StudentTestAttempt (محاولة الطالب)

**ده إيه؟**
- ده كل مرة الطالب يحل الاختبار
- الطالب ممكن يعيد الاختبار أكتر من مرة

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف المحاولة

### StudentProfileId (int)
- **ده إيه:** رقم بروفايل الطالب (Foreign Key)
- **العلاقة:** Many-to-One مع StudentProfile

### PersonalityTestId (int)
- **ده إيه:** رقم الاختبار (Foreign Key)
- **العلاقة:** Many-to-One مع PersonalityTest

### PersonalityTypeId (int?)
- **ده إيه:** نوع الشخصية اللي طلع (Foreign Key)
- **استخدامه:** النتيجة النهائية
- **العلاقة:** Many-to-One مع PersonalityType
- **ملاحظة:** بيبقى null لحد ما الطالب يخلص الاختبار

### StartedAt (DateTime)
- **ده إيه:** وقت بداية الاختبار
- **مثال:** `2025-01-15 10:00:00`

### CompletedAt (DateTime?)
- **ده إيه:** وقت انتهاء الاختبار
- **ملاحظة:** بيبقى null لو الطالب لسه مخلصش

### IsCompleted (bool)
- **ده إيه:** هل الاختبار اتكمل ولا لأ
- **القيم:** `true` أو `false`

### MlModelOutput (string?)
- **ده إيه:** نتيجة الـ ML Model (JSON)
- **استخدامه:** عشان نحفظ التفاصيل من الـ AI
- **مثال:** `{"confidence": 0.95, "scores": {...}}`
- **ملاحظة:** بنحفظه كـ JSON

---

## 5. StudentAnswer (إجابة الطالب)

**ده إيه؟**
- ده إجابة الطالب على كل سؤال
- كل سؤال ليه إجابة واحدة في كل محاولة

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الإجابة

### StudentTestAttemptId (int)
- **ده إيه:** رقم المحاولة (Foreign Key)
- **العلاقة:** Many-to-One مع StudentTestAttempt

### PersonalityQuestionId (int)
- **ده إيه:** رقم السؤال (Foreign Key)
- **العلاقة:** Many-to-One مع PersonalityQuestion

### AnswerValue (string)
- **ده إيه:** الإجابة نفسها
- **استخدامه:** القيمة اللي الطالب اختارها
- **أمثلة:**
  - لو Scale: `"5"` (من 1 لـ 5)
  - لو Multiple Choice: `"Alone"`
  - لو Yes/No: `"Yes"`

### AnsweredAt (DateTime)
- **ده إيه:** وقت الإجابة
- **مثال:** `2025-01-15 10:05:00`

---

## 6. PersonalityTypeTrack (ربط الشخصية بالمسار)

**ده إيه؟**
- ده جدول وسيط بيربط أنواع الشخصيات بالمسارات
- بيقول إن الشخصية دي تناسب المسار ده بنسبة كام

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الربط

### PersonalityTypeId (int)
- **ده إيه:** رقم نوع الشخصية (Foreign Key)
- **العلاقة:** Many-to-Many مع PersonalityType

### TrackId (int)
- **ده إيه:** رقم المسار (Foreign Key)
- **العلاقة:** Many-to-Many مع Track

### MatchScore (decimal)
- **ده إيه:** درجة التطابق
- **استخدامه:** عشان نعرف الشخصية دي تناسب المسار ده قد إيه
- **مثال:** `95.5` (يعني 95.5% مناسب)
- **النطاق:** من 0 لـ 100

---

## 7. TestRecommendation (التوصيات)

**ده إيه؟**
- ده أفضل 3 مسارات للطالب بناءً على نتيجة الاختبار
- كل محاولة تطلع 3 توصيات

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف التوصية

### StudentTestAttemptId (int)
- **ده إيه:** رقم المحاولة (Foreign Key)
- **العلاقة:** Many-to-One مع StudentTestAttempt

### TrackId (int)
- **ده إيه:** رقم المسار الموصى بيه (Foreign Key)
- **العلاقة:** Many-to-One مع Track

### Score (decimal)
- **ده إيه:** درجة التوصية
- **استخدامه:** عشان نعرف المسار ده مناسب قد إيه
- **مثال:** `95.5`

### Rank (int)
- **ده إيه:** ترتيب التوصية
- **استخدامه:** عشان نعرف ده أول ولا تاني ولا تالت اختيار
- **القيم:** `1`, `2`, `3`

---

## ملخص العلاقات

```
PersonalityTest (الاختبار)
  └─ 1:N → PersonalityQuestion (الأسئلة)

StudentProfile (الطالب)
  └─ 1:N → StudentTestAttempt (المحاولات)
      ├─ 1:N → StudentAnswer (الإجابات)
      ├─ N:1 → PersonalityType (النتيجة)
      └─ 1:N → TestRecommendation (التوصيات)

PersonalityType ↔ Track (M:N via PersonalityTypeTrack)
```

**مثال على الرحلة:**
```
1. الطالب Ahmed يبدأ الاختبار
   → StudentTestAttempt (IsCompleted = false)

2. يجاوب على الأسئلة
   → StudentAnswer (15 إجابة)

3. يخلص الاختبار
   → StudentTestAttempt (IsCompleted = true)
   → PersonalityType = ANALYTICAL

4. النظام يطلع التوصيات
   → TestRecommendation:
      - Rank 1: Backend Development (Score: 95.5)
      - Rank 2: Data Science (Score: 87.3)
      - Rank 3: DevOps (Score: 78.9)
```
