## Users

### 1. العلاقة بين `Users` و `StudentProfile` [1:1]

**الشرح:**
- اليوزر الواحد ممكن يبقى Student واحد فقط
- الـ Student الواحد أكيد هيبقى يوزر واحد

**التفاصيل:**
- **النوع:** One-to-One (1:1)
- **Nullable:** Yes (مش كل يوزر لازم يبقى student)

---

### 2. العلاقة بين `Users` و `MentorProfile` [1:1]

**الشرح:**
- اليوزر الواحد ممكن يبقى Mentor واحد فقط
- الـ Mentor الواحد أكيد هيبقى يوزر واحد

**التفاصيل:**
- **النوع:** One-to-One (1:1)
- **Foreign :** `MentorProfile.UserId` → `Users.Id`
- **Nullable:** Yes (مش كل يوزر لازم يبقى mentor)

---

### 3. العلاقة بين `Users` و `Notification` [1:N]

**الشرح:**
- اليوزر الواحد ممكن يستقبل notifications كتير
- الـ Notification الواحدة أكيد هتبقى ليوزر واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل notification لازم يبقى ليها user)

---

---


## Personality Test System

### 1. العلاقة بين `PersonalityTest` و `PersonalityQuestion` [1:N]

**الشرح:**
- الاختبار الواحد فيه أسئلة كتير (10-15 سؤال)
- السؤال الواحد أكيد هيبقى في اختبار واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل سؤال لازم يبقى ليه اختبار)

---

### 2. العلاقة بين `StudentProfile` و `StudentTestAttempt` [1:N]

**الشرح:**
- الطالب الواحد ممكن يعمل محاولات كتير للاختبار (يعيد الاختبار)
- المحاولة الواحدة أكيد هتبقى لطالب واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل محاولة لازم يبقى ليها طالب)

---

### 3. العلاقة بين `PersonalityTest` و `StudentTestAttempt` [1:N]

**الشرح:**
- الاختبار الواحد ممكن طلاب كتير يحلوه (محاولات كتير)
- المحاولة الواحدة أكيد هتبقى لاختبار واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل محاولة لازم يبقى ليها اختبار)

---

### 4. العلاقة بين `StudentTestAttempt` و `PersonalityType` [N:1]

**الشرح:**
- محاولات كتير ممكن تطلع نفس نوع الشخصية
- المحاولة الواحدة أكيد هتطلع نوع شخصية واحد بس

**التفاصيل:**
- **النوع:** Many-to-One (N:1)
- **Nullable:** Yes (لحد ما الاختبار يتكمل)

---

### 5. العلاقة بين `StudentTestAttempt` و `StudentAnswer` [1:N]

**الشرح:**
- المحاولة الواحدة فيها إجابات كتير (إجابة لكل سؤال)
- الإجابة الواحدة أكيد هتبقى في محاولة واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل إجابة لازم يبقى ليها محاولة)

---

### 6. العلاقة بين `PersonalityQuestion` و `StudentAnswer` [1:N]

**الشرح:**
- السؤال الواحد ممكن طلاب كتير يجاوبوا عليه (إجابات كتير)
- الإجابة الواحدة أكيد هتبقى لسؤال واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل إجابة لازم يبقى ليها سؤال)

---

### 7. العلاقة بين `StudentTestAttempt` و `TestRecommendation` [1:N]

**الشرح:**
- المحاولة الواحدة تطلع 3 توصيات (أفضل 3 مسارات)
- التوصية الواحدة أكيد هتبقى لمحاولة واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل توصية لازم يبقى ليها محاولة)

---

### 8. العلاقة بين `Track` و `TestRecommendation` [1:N]

**الشرح:**
- المسار الواحد ممكن يتوصى بيه لطلاب كتير
- التوصية الواحدة أكيد هتبقى لمسار واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل توصية لازم يبقى ليها مسار)

---

### 9. العلاقة بين `PersonalityType` و `Track` [M:N] عن طريق `PersonalityTypeTrack`

**الشرح:**
- نوع الشخصية الواحد ممكن يناسب مسارات كتير
- المسار الواحد ممكن يناسب أنواع شخصيات كتير

**التفاصيل:**
- **النوع:** Many-to-Many (M:N)
- **Junction Table:** `PersonalityTypeTrack`
- **Foreign Keys:** 
  - `PersonalityTypeTrack.PersonalityTypeId` → `PersonalityType.Id`
  - `PersonalityTypeTrack.TrackId` → `Track.Id`


---

## علاقات Tracks & Roadmaps

### 1. العلاقة بين `Track` و `Roadmap` [1:N]

**الشرح:**
- المسار الواحد فيه roadmaps كتير (مستويات مختلفة: Beginner, Intermediate, Advanced)
- الـ Roadmap الواحدة أكيد هتبقى لمسار واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل roadmap لازم يبقى ليها track)

---

### 2. العلاقة بين `Roadmap` و `RoadmapMilestone` [1:N]

**الشرح:**
- الـ Roadmap الواحدة فيها milestones كتير (مراحل رئيسية)
- الـ Milestone الواحدة أكيد هتبقى في roadmap واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل milestone لازم يبقى ليها roadmap)

---

### 3. العلاقة بين `RoadmapMilestone` و `MilestoneTask` [1:N]

**الشرح:**
- الـ Milestone الواحدة فيها tasks كتير (مهام فردية)
- الـ Task الواحدة أكيد هتبقى في milestone واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل task لازم يبقى ليها milestone)

---

### 4. العلاقة بين `MilestoneTask` و `TaskResource` [1:N]

**الشرح:**
- الـ Task الواحدة فيها resources كتير (فيديوهات، مقالات، كورسات)
- الـ Resource الواحد أكيد هيبقى لـ task واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل resource لازم يبقى ليه task)

---

### 5. العلاقة بين `StudentProfile` و `StudentRoadmap` [1:N]

**الشرح:**
- الطالب الواحد ممكن يتابع roadmaps كتير (مسارات مختلفة)
- الـ StudentRoadmap الواحدة أكيد هتبقى لطالب واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل student roadmap لازم يبقى ليها طالب)

---

### 6. العلاقة بين `Roadmap` و `StudentRoadmap` [1:N]

**الشرح:**
- الـ Roadmap Template الواحدة ممكن طلاب كتير ينسخوها
- الـ StudentRoadmap الواحدة أكيد هتبقى منسوخة من roadmap واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل student roadmap لازم يبقى ليها roadmap template)

---

### 7. العلاقة بين `StudentProfile` و `MilestoneTask` [M:N] عن طريق `StudentProgress`

**الشرح:**
- الطالب الواحد ممكن يكمل tasks كتير
- الـ Task الواحدة ممكن طلاب كتير يكملوها

**التفاصيل:**
- **النوع:** Many-to-Many (M:N)
- **Junction Table:** `StudentProgress`
- **Foreign Keys:**
  - `StudentProgress.StudentProfileId` → `StudentProfile.Id`
  - `StudentProgress.MilestoneTaskId` → `MilestoneTask.Id`

---

### 8. العلاقة بين `MentorProfile` و `Track` [M:N] عن طريق `MentorTrack`

**الشرح:**
- المرشد الواحد ممكن يتخصص في مسارات كتير
- المسار الواحد ممكن مرشدين كتير يتخصصوا فيه

**التفاصيل:**
- **النوع:** Many-to-Many (M:N)
- **Junction Table:** `MentorTrack`
- **Foreign Keys:**
  - `MentorTrack.MentorProfileId` → `MentorProfile.Id`
  - `MentorTrack.TrackId` → `Track.Id`

---

### 9. العلاقة بين `MentorProfile` و `Track` [M:N] عن طريق `MentorTrackLevel`

**الشرح:**
- المرشد الواحد ممكن يدرّس نفس المسار في مستويات مختلفة
- المسار الواحد ممكن مرشدين كتير يدرسوه في مستويات مختلفة

**التفاصيل:**
- **النوع:** Many-to-Many (M:N)
- **Junction Table:** `MentorTrackLevel`
- **Foreign Keys:**
  - `MentorTrackLevel.MentorProfileId` → `MentorProfile.Id`
  - `MentorTrackLevel.TrackId` → `Track.Id`

---

## علاقات Mentorship System

### 1. العلاقة بين `StudentProfile` و `MentorshipRequest` [1:N]

**الشرح:**
- الطالب الواحد ممكن يبعت طلبات إرشاد كتير (لمرشدين مختلفين أو نفس المرشد)
- الطلب الواحد أكيد هيبقى من طالب واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل طلب لازم يبقى ليه طالب)

---

### 2. العلاقة بين `MentorProfile` و `MentorshipRequest` [1:N]

**الشرح:**
- المرشد الواحد ممكن يستقبل طلبات إرشاد كتير (من طلاب مختلفين)
- الطلب الواحد أكيد هيبقى لمرشد واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل طلب لازم يبقى ليه مرشد)

---

### 3. العلاقة بين `Track` و `MentorshipRequest` [1:N]

**الشرح:**
- المسار الواحد ممكن يتطلب فيه إرشاد كتير
- الطلب الواحد أكيد هيبقى لمسار واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل طلب لازم يبقى ليه مسار)

---

### 4. العلاقة بين `MentorshipRequest` و `MentorshipSession` [1:1]

**الشرح:**
- الطلب الواحد لو اتقبل بيتحول لجلسة إرشاد واحدة
- الجلسة الواحدة أكيد هتبقى من طلب واحد بس

**التفاصيل:**
- **النوع:** One-to-One (1:1)
- **Nullable:** No (كل جلسة لازم يبقى ليها طلب)

---

### 5. العلاقة بين `StudentProfile` و `MentorshipSession` [1:N]

**الشرح:**
- الطالب الواحد ممكن يبقى في جلسات إرشاد كتير (مع مرشدين مختلفين)
- الجلسة الواحدة أكيد هتبقى لطالب واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل جلسة لازم يبقى ليها طالب)

---

### 6. العلاقة بين `MentorProfile` و `MentorshipSession` [1:N]

**الشرح:**
- المرشد الواحد ممكن يبقى في جلسات إرشاد كتير (مع طلاب مختلفين)
- الجلسة الواحدة أكيد هتبقى لمرشد واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل جلسة لازم يبقى ليها مرشد)

---

### 7. العلاقة بين `Track` و `MentorshipSession` [1:N]

**الشرح:**
- المسار الواحد ممكن يتدرس في جلسات كتير
- الجلسة الواحدة أكيد هتبقى لمسار واحد بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل جلسة لازم يبقى ليها مسار)


---

## علاقات Communication System

### 8. العلاقة بين `MentorshipSession` و `Message` [1:N]

**الشرح:**
- الجلسة الواحدة فيها رسائل كتير (محادثة بين الطالب والمرشد)
- الرسالة الواحدة أكيد هتبقى في جلسة واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل رسالة لازم يبقى ليها جلسة)

---

### 9. العلاقة بين `StudentProfile` و `Message` [1:N]

**الشرح:**
- الطالب الواحد ممكن يبعت رسائل كتير
- الرسالة الواحدة ممكن تبقى من طالب واحد بس (أو من مرشد)

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** Yes (الرسالة يا من طالب يا من مرشد)

---

### 10. العلاقة بين `MentorProfile` و `Message` [1:N]

**الشرح:**
- المرشد الواحد ممكن يبعت رسائل كتير
- الرسالة الواحدة ممكن تبقى من مرشد واحد بس (أو من طالب)

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** Yes (الرسالة يا من طالب يا من مرشد)

---

### 11. العلاقة بين `Message` و `MessageAttachment` [1:N]

**الشرح:**
- الرسالة الواحدة ممكن يبقى فيها مرفقات كتير (صور، ملفات، فيديوهات)
- المرفق الواحد أكيد هيبقى لرسالة واحدة بس

**التفاصيل:**
- **النوع:** One-to-Many (1:N)
- **Nullable:** No (كل مرفق لازم يبقى ليه رسالة)

---
