# شرح Entities - Student Progress & Mentorship Module

## Student Progress (تقدم الطالب)

### 1. StudentRoadmap (نسخة الطالب من الـ Roadmap)

**ده إيه؟**
- ده نسخة خاصة بالطالب من الـ Roadmap Template
- كل طالب بينسخ الـ roadmap ويشتغل عليها لوحده

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف نسخة الطالب

#### StudentProfileId (int)
- **ده إيه:** رقم بروفايل الطالب (Foreign Key)
- **العلاقة:** Many-to-One مع StudentProfile

#### RoadmapId (int)
- **ده إيه:** رقم الـ Roadmap الأصلي (Foreign Key)
- **العلاقة:** Many-to-One مع Roadmap
- **ملاحظة:** ده الـ template اللي الطالب نسخ منه

#### StartDate (DateTime)
- **ده إيه:** تاريخ البداية
- **استخدامه:** إمتى الطالب بدأ الـ roadmap
- **مثال:** `2025-01-15`

#### TargetEndDate (DateTime?)
- **ده إيه:** التاريخ المستهدف للانتهاء
- **استخدامه:** الطالب عايز يخلص إمتى
- **مثال:** `2025-04-15` (بعد 3 شهور)
- **ملاحظة:** مش إجباري

#### ProgressPercentage (decimal)
- **ده إيه:** نسبة الإنجاز
- **استخدامه:** عشان نعرف الطالب وصل لفين
- **مثال:** `45.5` (يعني 45.5% خلص)
- **النطاق:** من 0 لـ 100

#### IsActive (bool)
- **ده إيه:** هل الطالب بيشتغل على الـ roadmap ده دلوقتي ولا لأ
- **استخدامه:** الطالب ممكن يبقى عنده أكتر من roadmap بس واحد بس active

**مثال:**
```
Student: Ahmed
  ├─ Backend Development (Active, 45% complete)
  ├─ DevOps (Inactive, 10% complete)
  └─ Data Science (Inactive, 0% complete)
```

---

### 2. StudentProgress (تقدم الطالب في المهام)

**ده إيه؟**
- ده جدول وسيط بيتتبع الطالب كمّل أنهي tasks
- كل طالب عنده progress منفصل لكل task

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف الـ Progress

#### StudentProfileId (int)
- **ده إيه:** رقم بروفايل الطالب (Foreign Key)
- **العلاقة:** Many-to-Many مع StudentProfile

#### MilestoneTaskId (int)
- **ده إيه:** رقم الـ Task (Foreign Key)
- **العلاقة:** Many-to-Many مع MilestoneTask

#### IsCompleted (bool)
- **ده إيه:** هل الطالب خلص الـ task ده ولا لأ
- **القيم:** `true` أو `false`

#### CompletedAt (DateTime?)
- **ده إيه:** تاريخ إتمام الـ Task
- **ملاحظة:** بيبقى null لو الطالب لسه مخلصش

#### TimeSpentHours (decimal)
- **ده إيه:** الوقت اللي الطالب قضاه في الـ Task
- **استخدامه:** عشان نتتبع الطالب قضى كام ساعة فعلياً
- **مثال:** `12.5` (12 ساعة ونص)

#### Notes (string?)
- **ده إيه:** ملاحظات الطالب
- **استخدامه:** الطالب ممكن يكتب ملاحظات أو تعليقات
- **مثال:** `Great tutorial, learned a lot!`
- **ملاحظة:** مش إجباري

**مثال:**
```
Student: Ahmed
  Task: Learn C# Basics
    ├─ IsCompleted: true
    ├─ CompletedAt: 2025-01-20
    ├─ TimeSpentHours: 12
    └─ Notes: "Finished all exercises"

  Task: OOP Concepts
    ├─ IsCompleted: false
    ├─ CompletedAt: null
    ├─ TimeSpentHours: 5
    └─ Notes: "Still working on it"
```

---

## Mentorship System (نظام الإرشاد)

### 3. MentorshipRequest (طلب الإرشاد)

**ده إيه؟**
- ده الطلب اللي الطالب بيبعته للمرشد عشان يطلب إرشاد
- المرشد ممكن يقبل أو يرفض الطلب

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف الطلب

#### StudentProfileId (int)
- **ده إيه:** رقم بروفايل الطالب (Foreign Key)
- **العلاقة:** Many-to-One مع StudentProfile

#### MentorProfileId (int)
- **ده إيه:** رقم بروفايل المرشد (Foreign Key)
- **العلاقة:** Many-to-One مع MentorProfile

#### TrackId (int)
- **ده إيه:** رقم المسار اللي الطالب عايز إرشاد فيه (Foreign Key)
- **العلاقة:** Many-to-One مع Track

#### Message (string)
- **ده إيه:** رسالة الطالب للمرشد
- **استخدامه:** الطالب بيعرّف نفسه ويقول عايز إيه
- **مثال:** `Hi, I need help with backend development`

#### Status (MentorshipRequestStatus)
- **ده إيه:** حالة الطلب
- **القيم:**
  - `Pending = 1` (معلق - لسه المرشد مردش)
  - `Accepted = 2` (مقبول)
  - `Rejected = 3` (مرفوض)
  - `Cancelled = 4` (ملغي من الطالب)

#### RequestedAt (DateTime)
- **ده إيه:** تاريخ إرسال الطلب
- **مثال:** `2025-01-15 10:00:00`

#### RespondedAt (DateTime?)
- **ده إيه:** تاريخ رد المرشد
- **ملاحظة:** بيبقى null لو المرشد لسه مردش

#### ResponseMessage (string?)
- **ده إيه:** رد المرشد
- **استخدامه:** المرشد بيكتب سبب القبول أو الرفض
- **مثال:** `Sure, I'd love to help you!`
- **ملاحظة:** مش إجباري

**مثال:**
```
Request from Ahmed to Sara:
  ├─ Track: Backend Development
  ├─ Message: "Hi, I need help with C#"
  ├─ Status: Accepted
  ├─ RequestedAt: 2025-01-15 10:00
  ├─ RespondedAt: 2025-01-15 14:00
  └─ ResponseMessage: "Sure, let's start!"
```

---

### 4. MentorshipSession (جلسة الإرشاد)

**ده إيه؟**
- ده الجلسة الفعلية بين الطالب والمرشد
- بتتعمل لما المرشد يقبل الطلب

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف الجلسة

#### MentorshipRequestId (int)
- **ده إيه:** رقم الطلب اللي الجلسة دي جاية منه (Foreign Key)
- **العلاقة:** One-to-One مع MentorshipRequest

#### StudentProfileId (int)
- **ده إيه:** رقم بروفايل الطالب (Foreign Key)

#### MentorProfileId (int)
- **ده إيه:** رقم بروفايل المرشد (Foreign Key)

#### TrackId (int)
- **ده إيه:** رقم المسار (Foreign Key)

#### StartDate (DateTime)
- **ده إيه:** تاريخ بداية الجلسة
- **مثال:** `2025-01-16`

#### EndDate (DateTime?)
- **ده إيه:** تاريخ انتهاء الجلسة
- **ملاحظة:** بيبقى null لو الجلسة لسه شغالة

#### Status (MentorshipSessionStatus)
- **ده إيه:** حالة الجلسة
- **القيم:**
  - `Active = 1` (نشطة)
  - `Completed = 2` (منتهية)
  - `Cancelled = 3` (ملغية)

#### Rating (decimal?)
- **ده إيه:** تقييم الطالب للمرشد
- **استخدامه:** الطالب بيقيّم المرشد بعد ما الجلسة تخلص
- **مثال:** `4.5` (من 5)
- **النطاق:** من 1 لـ 5
- **ملاحظة:** بيبقى null لو الطالب لسه معملش review

#### Review (string?)
- **ده إيه:** مراجعة الطالب
- **استخدامه:** الطالب بيكتب رأيه في المرشد
- **مثال:** `Great mentor, very helpful!`
- **ملاحظة:** مش إجباري

#### ReviewedAt (DateTime?)
- **ده إيه:** تاريخ كتابة المراجعة
- **ملاحظة:** بيبقى null لو الطالب لسه معملش review

**مثال:**
```
Session between Ahmed and Sara:
  ├─ Track: Backend Development
  ├─ StartDate: 2025-01-16
  ├─ Status: Completed
  ├─ EndDate: 2025-02-16
  ├─ Rating: 4.5
  ├─ Review: "Excellent mentor!"
  └─ ReviewedAt: 2025-02-16
```

---

## Communication (التواصل)

### 5. Message (الرسالة)

**ده إيه؟**
- ده الرسائل اللي بتتبعت بين الطالب والمرشد في الجلسة
- زي الشات العادي

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف الرسالة

#### MentorshipSessionId (int)
- **ده إيه:** رقم الجلسة (Foreign Key)
- **العلاقة:** Many-to-One مع MentorshipSession

#### SenderStudentProfileId (int?)
- **ده إيه:** رقم الطالب اللي بعت الرسالة (Foreign Key)
- **ملاحظة:** بيبقى null لو المرشد هو اللي بعت

#### SenderMentorProfileId (int?)
- **ده إيه:** رقم المرشد اللي بعت الرسالة (Foreign Key)
- **ملاحظة:** بيبقى null لو الطالب هو اللي بعت

#### Content (string)
- **ده إيه:** محتوى الرسالة
- **مثال:** `How do I implement dependency injection?`

#### IsRead (bool)
- **ده إيه:** هل الرسالة اتقرت ولا لأ
- **استخدامه:** عشان نعرض "unread messages"

#### ReadAt (DateTime?)
- **ده إيه:** تاريخ قراءة الرسالة
- **ملاحظة:** بيبقى null لو الرسالة لسه متقرتش

#### SentAt (DateTime)
- **ده إيه:** تاريخ إرسال الرسالة
- **مثال:** `2025-01-16 10:30:00`

**مثال:**
```
Session Chat:
  ├─ Message 1 (Ahmed): "Hi, I need help with DI"
  ├─ Message 2 (Sara): "Sure! Let me explain..."
  ├─ Message 3 (Ahmed): "Thanks! That's clear now"
  └─ Message 4 (Sara): "You're welcome!"
```

---

### 6. MessageAttachment (مرفق الرسالة)

**ده إيه؟**
- ده الملفات اللي بتتبعت مع الرسائل (صور، ملفات، فيديوهات)

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف المرفق

#### MessageId (int)
- **ده إيه:** رقم الرسالة (Foreign Key)
- **العلاقة:** Many-to-One مع Message

#### FileName (string)
- **ده إيه:** اسم الملف
- **مثال:** `code_example.cs`

#### FileUrl (string)
- **ده إيه:** رابط الملف
- **مثال:** `https://storage.com/attachments/code_example.cs`

#### FileSize (long)
- **ده إيه:** حجم الملف بالـ bytes
- **مثال:** `5120` (5 KB)

#### AttachmentType (MessageAttachmentType)
- **ده إيه:** نوع المرفق
- **القيم:**
  - `Image = 1` (صورة)
  - `Document = 2` (مستند)
  - `Video = 3` (فيديو)
  - `Other = 4` (أخرى)

#### UploadedAt (DateTime)
- **ده إيه:** تاريخ رفع الملف
- **مثال:** `2025-01-16 10:35:00`

**مثال:**
```
Message: "Here's the code example"
  Attachments:
    ├─ code_example.cs (Document, 5 KB)
    ├─ screenshot.png (Image, 120 KB)
    └─ tutorial.mp4 (Video, 2 MB)
```

---

### 7. Notification (الإشعارات)

**ده إيه؟**
- ده الإشعارات اللي بتوصل لليوزر
- زي: "طلب الإرشاد اتقبل"، "رسالة جديدة"، إلخ

**الـ Properties:**

#### Id (int)
- **ده إيه:** رقم تعريف الإشعار

#### UserId (Guid)
- **ده إيه:** رقم اليوزر (Foreign Key)
- **العلاقة:** Many-to-One مع Users

#### Type (NotificationType)
- **ده إيه:** نوع الإشعار
- **القيم:**
  - `MentorshipRequest = 1`
  - `MentorshipAccepted = 2`
  - `MentorshipRejected = 3`
  - `NewMessage = 4`
  - `TaskCompleted = 5`
  - `MilestoneCompleted = 6`
  - `RoadmapCreated = 7`
  - `General = 8`

#### TitleEn (string)
- **ده إيه:** عنوان الإشعار بالإنجليزي
- **مثال:** `Mentorship Request Accepted`

#### TitleAr (string)
- **ده إيه:** عنوان الإشعار بالعربي
- **مثال:** `تم قبول طلب الإرشاد`

#### MessageEn (string)
- **ده إيه:** محتوى الإشعار بالإنجليزي
- **مثال:** `Sara accepted your mentorship request`

#### MessageAr (string)
- **ده إيه:** محتوى الإشعار بالعربي
- **مثال:** `سارة قبلت طلب الإرشاد الخاص بك`

#### IsRead (bool)
- **ده إيه:** هل الإشعار اتقرى ولا لأ

#### ReadAt (DateTime?)
- **ده إيه:** تاريخ قراءة الإشعار

#### RelatedEntityId (string?)
- **ده إيه:** رقم الـ entity المرتبط
- **استخدامه:** عشان لو اليوزر دوس على الإشعار نوديه للصفحة الصح
- **مثال:** `"123"` (رقم الـ MentorshipRequest)

#### RelatedEntityType (string?)
- **ده إيه:** نوع الـ entity المرتبط
- **مثال:** `"MentorshipRequest"`, `"Message"`

**مثال:**
```
Notification for Ahmed:
  ├─ Type: MentorshipAccepted
  ├─ Title: "تم قبول طلب الإرشاد"
  ├─ Message: "سارة قبلت طلب الإرشاد الخاص بك"
  ├─ IsRead: false
  └─ RelatedEntityId: "123" (MentorshipRequest)
```

---

## ملخص الرحلة الكاملة

```
1. الطالب ينسخ Roadmap
   → StudentRoadmap

2. يبدأ يشتغل على Tasks
   → StudentProgress

3. يطلب مساعدة من مرشد
   → MentorshipRequest

4. المرشد يقبل
   → MentorshipSession

5. يتواصلوا
   → Message + MessageAttachment

6. الطالب يقيّم المرشد
   → MentorshipSession.Rating

7. إشعارات في كل خطوة
   → Notification
```
