# شرح Entities - Tracks & Roadmaps Module

## 1. Track (المسار التعليمي)

**ده إيه؟**
- ده المسار اللي الطالب هيتعلمه (زي Backend, Frontend, DevOps)
- كل track فيه roadmaps بمستويات مختلفة

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف المسار

### Code (string)
- **ده إيه:** كود المسار
- **استخدامه:** اسم مختصر unique
- **مثال:** `BACKEND`, `FRONTEND`
- **ملاحظة:** لازم يكون Unique

### Name (string)
- **ده إيه:** i18n key للاسم
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `track.backend.name`
- **الترجمة:**
  - English: `Backend Development`
  - Arabic: `تطوير الباك إند`

### Description (string)
- **ده إيه:** i18n key للوصف
- **استخدامه:** الـ Frontend يترجمه حسب اللغة
- **مثال:** `track.backend.description`
- **الترجمة:**
  - English: `Learn server-side development with .NET`
  - Arabic: `تعلم تطوير السيرفر باستخدام .NET`

### IconUrl (string?)
- **ده إيه:** رابط أيقونة المسار
- **مثال:** `https://storage.com/icons/backend.svg`
- **ملاحظة:** مش إجباري

### IsActive (bool)
- **ده إيه:** هل المسار نشط ولا لأ
- **استخدامه:** عشان نقدر نخفي مسارات معينة

---

## 2. MentorTrack (تخصصات المرشد)

**ده إيه؟**
- ده جدول وسيط بيربط المرشد بالمسارات اللي بيتخصص فيها
- المرشد ممكن يتخصص في أكتر من مسار

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف التخصص

### MentorProfileId (int)
- **ده إيه:** رقم بروفايل المرشد (Foreign Key)
- **العلاقة:** Many-to-Many مع MentorProfile

### TrackId (int)
- **ده إيه:** رقم المسار (Foreign Key)
- **العلاقة:** Many-to-Many مع Track

### IsActive (bool)
- **ده إيه:** هل التخصص ده نشط ولا لأ
- **استخدامه:** المرشد ممكن يوقف تخصص معين مؤقتاً

**مثال:**
```
Mentor: Sara
  ├─ Backend Development (Active)
  ├─ DevOps (Active)
  └─ Cloud Computing (Inactive)
```

---

## 3. MentorTrackLevel (مستويات تدريس المرشد)

**ده إيه؟**
- ده بيحدد المرشد بيدرّس المسار ده في أنهي مستوى
- نفس المرشد ممكن يدرّس نفس المسار في مستويات مختلفة

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف المستوى

### MentorProfileId (int)
- **ده إيه:** رقم بروفايل المرشد (Foreign Key)

### TrackId (int)
- **ده إيه:** رقم المسار (Foreign Key)

### Level (TrackLevel)
- **ده إيه:** المستوى
- **القيم:**
  - `Beginner = 1` (مبتدئ)
  - `Intermediate = 2` (متوسط)
  - `Advanced = 3` (متقدم)

### YearsOfExperience (int)
- **ده إيه:** سنين الخبرة في المستوى ده
- **استخدامه:** عشان نعرف المرشد عنده خبرة قد إيه في المستوى ده
- **مثال:** `5` (5 سنين خبرة في المستوى المتوسط)

**مثال:**
```
Mentor: Sara (10 years total experience)
  Backend Development:
    ├─ Beginner (2 years exp)
    ├─ Intermediate (5 years exp)
    └─ Advanced (10 years exp)
```

---

## 4. Roadmap (خريطة الطريق)

**ده إيه؟**
- ده الخطة التفصيلية لتعلم المسار
- كل track فيه roadmaps لمستويات مختلفة
- فيه roadmaps جاهزة (Templates) والطالب بينسخها

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الـ Roadmap

### TrackId (int)
- **ده إيه:** رقم المسار (Foreign Key)
- **العلاقة:** Many-to-One مع Track

### TitleEn (string)
- **ده إيه:** عنوان الـ Roadmap بالإنجليزي
- **مثال:** `Backend Development - Beginner Level`

### TitleAr (string)
- **ده إيه:** عنوان الـ Roadmap بالعربي
- **مثال:** `تطوير الباك إند - مستوى مبتدئ`

### DescriptionEn (string)
- **ده إيه:** وصف الـ Roadmap بالإنجليزي

### DescriptionAr (string)
- **ده إيه:** وصف الـ Roadmap بالعربي

### Level (TrackLevel)
- **ده إيه:** مستوى الـ Roadmap
- **القيم:** `Beginner`, `Intermediate`, `Advanced`

### CreatorType (RoadmapCreatorType)
- **ده إيه:** مين اللي عمل الـ Roadmap
- **القيم:**
  - `System = 1` (النظام/الأدمن)
  - `Mentor = 2` (مرشد)
  - `Student = 3` (طالب)

### IsTemplate (bool)
- **ده إيه:** هل ده template جاهز ولا نسخة شخصية
- **استخدامه:** الـ Templates بتظهر للطلاب عشان ينسخوها

### IsActive (bool)
- **ده إيه:** هل الـ Roadmap نشط ولا لأ

---

## 5. RoadmapMilestone (المرحلة الرئيسية)

**ده إيه؟**
- ده مرحلة كبيرة في الـ Roadmap
- كل roadmap فيها milestones (مراحل)
- كل milestone فيها tasks (مهام)

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الـ Milestone

### RoadmapId (int)
- **ده إيه:** رقم الـ Roadmap (Foreign Key)
- **العلاقة:** Many-to-One مع Roadmap

### TitleEn (string)
- **ده إيه:** عنوان المرحلة بالإنجليزي
- **مثال:** `Programming Fundamentals`

### TitleAr (string)
- **ده إيه:** عنوان المرحلة بالعربي
- **مثال:** `أساسيات البرمجة`

### DescriptionEn (string)
- **ده إيه:** وصف المرحلة بالإنجليزي

### DescriptionAr (string)
- **ده إيه:** وصف المرحلة بالعربي

### OrderIndex (int)
- **ده إيه:** ترتيب المرحلة
- **استخدامه:** عشان نعرض المراحل بالترتيب
- **مثال:** `1`, `2`, `3`

### EstimatedDurationDays (int)
- **ده إيه:** المدة المتوقعة بالأيام
- **استخدامه:** عشان نقول للطالب المرحلة دي هتاخد كام يوم
- **مثال:** `30` (شهر)

**مثال:**
```
Roadmap: Backend Development - Beginner
  ├─ Milestone 1: Programming Fundamentals (30 days)
  ├─ Milestone 2: Web Development Basics (30 days)
  ├─ Milestone 3: Database Fundamentals (20 days)
  └─ Milestone 4: First Project (10 days)
```

---

## 6. MilestoneTask (المهمة)

**ده إيه؟**
- ده مهمة صغيرة داخل المرحلة
- كل milestone فيها tasks كتير
- الطالب بيكمل task ورا task

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الـ Task

### RoadmapMilestoneId (int)
- **ده إيه:** رقم الـ Milestone (Foreign Key)
- **العلاقة:** Many-to-One مع RoadmapMilestone

### TitleEn (string)
- **ده إيه:** عنوان المهمة بالإنجليزي
- **مثال:** `Learn C# Basics`

### TitleAr (string)
- **ده إيه:** عنوان المهمة بالعربي
- **مثال:** `تعلم أساسيات C#`

### DescriptionEn (string)
- **ده إيه:** وصف المهمة بالإنجليزي

### DescriptionAr (string)
- **ده إيه:** وصف المهمة بالعربي

### OrderIndex (int)
- **ده إيه:** ترتيب المهمة داخل الـ Milestone

### EstimatedHours (decimal)
- **ده إيه:** الساعات المتوقعة
- **استخدامه:** عشان نقول للطالب المهمة دي هتاخد كام ساعة
- **مثال:** `10.5` (10 ساعات ونص)

### IsOptional (bool)
- **ده إيه:** هل المهمة اختيارية ولا إجبارية
- **استخدامه:** بعض المهام ممكن تكون bonus

**مثال:**
```
Milestone: Programming Fundamentals
  ├─ Task 1: Learn C# Basics (10 hours, Required)
  ├─ Task 2: OOP Concepts (15 hours, Required)
  ├─ Task 3: Data Structures (20 hours, Required)
  └─ Task 4: Advanced Topics (10 hours, Optional)
```

---

## 7. TaskResource (المصدر التعليمي)

**ده إيه؟**
- ده الموارد اللي الطالب هيتعلم منها (فيديوهات، مقالات، كورسات)
- كل task فيها resources كتير

**الـ Properties:**

### Id (int)
- **ده إيه:** رقم تعريف الـ Resource

### MilestoneTaskId (int)
- **ده إيه:** رقم الـ Task (Foreign Key)
- **العلاقة:** Many-to-One مع MilestoneTask

### TitleEn (string)
- **ده إيه:** عنوان المصدر بالإنجليزي
- **مثال:** `C# Tutorial for Beginners`

### TitleAr (string)
- **ده إيه:** عنوان المصدر بالعربي
- **مثال:** `شرح C# للمبتدئين`

### ResourceType (ResourceType)
- **ده إيه:** نوع المصدر
- **القيم:**
  - `Video = 1` (فيديو)
  - `Article = 2` (مقال)
  - `Book = 3` (كتاب)
  - `Course = 4` (كورس)
  - `Documentation = 5` (توثيق)
  - `Project = 6` (مشروع)
  - `Tool = 7` (أداة)
  - `Other = 8` (أخرى)

### Url (string)
- **ده إيه:** رابط المصدر
- **مثال:** `https://youtube.com/watch?v=xxx`

### IsFree (bool)
- **ده إيه:** هل المصدر مجاني ولا مدفوع
- **استخدامه:** عشان نوضح للطالب

### OrderIndex (int)
- **ده إيه:** ترتيب المصدر
- **استخدامه:** عشان نرتب المصادر حسب الأهمية

**مثال:**
```
Task: Learn C# Basics
  ├─ Resource 1: C# Tutorial (YouTube, Free, Video)
  ├─ Resource 2: C# Documentation (Microsoft, Free, Documentation)
  ├─ Resource 3: C# Course (Udemy, Paid, Course)
  └─ Resource 4: Practice Exercises (LeetCode, Free, Project)
```

---

## ملخص الهيكل الكامل

```
Track (المسار)
  └─ Roadmap (خريطة الطريق)
      └─ RoadmapMilestone (المرحلة)
          └─ MilestoneTask (المهمة)
              └─ TaskResource (المصدر)

مثال:
Backend Development
  └─ Backend - Beginner
      └─ Programming Fundamentals (30 days)
          └─ Learn C# Basics (10 hours)
              ├─ Video Tutorial
              ├─ Documentation
              └─ Practice Exercises
```
