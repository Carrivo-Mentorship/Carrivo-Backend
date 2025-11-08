# دليل التحويل لـ i18n

## 🎯 التغيير الأساسي

تم إزالة جميع الـ columns المكررة (En/Ar) واستبدالها بـ **i18n keys** عشان نتعامل مع الترجمة في الـ Frontend.

---

## ✅ الـ Entities المُعدّلة

### 1. PersonalityType
```csharp
// ❌ قبل
public string NameEn { get; set; }
public string NameAr { get; set; }
public string DescriptionEn { get; set; }
public string DescriptionAr { get; set; }

// ✅ بعد
public string Code { get; set; } // "ANALYTICAL"
public string NameKey { get; set; } // "personality.analytical.name"
public string DescriptionKey { get; set; } // "personality.analytical.description"
public string? IconUrl { get; set; }
public bool IsActive { get; set; }
```

### 2. PersonalityTest
```csharp
// ❌ قبل
public string TitleEn { get; set; }
public string TitleAr { get; set; }
public string DescriptionEn { get; set; }
public string DescriptionAr { get; set; }
public int Version { get; set; }

// ✅ بعد
public string Version { get; set; } // "1.0"
public string TitleKey { get; set; } // "test.personality.v1.title"
public string DescriptionKey { get; set; } // "test.personality.v1.description"
```

### 3. PersonalityQuestion
```csharp
// ❌ قبل
public string QuestionTextEn { get; set; }
public string QuestionTextAr { get; set; }

// ✅ بعد
public string QuestionKey { get; set; } // "test.question.1.text"
```

### 4. Track
```csharp
// ❌ قبل
public string NameEn { get; set; }
public string NameAr { get; set; }
public string DescriptionEn { get; set; }
public string DescriptionAr { get; set; }

// ✅ بعد
public string Code { get; set; } // "BACKEND"
public string NameKey { get; set; } // "track.backend.name"
public string DescriptionKey { get; set; } // "track.backend.description"
```

### 5. Roadmap
```csharp
// ❌ قبل
public string TitleEn { get; set; }
public string TitleAr { get; set; }
public string DescriptionEn { get; set; }
public string DescriptionAr { get; set; }

// ✅ بعد
public string TitleKey { get; set; } // "roadmap.backend.beginner.title"
public string DescriptionKey { get; set; } // "roadmap.backend.beginner.description"
```

### 6. RoadmapMilestone
```csharp
// ❌ قبل
public string TitleEn { get; set; }
public string TitleAr { get; set; }
public string DescriptionEn { get; set; }
public string DescriptionAr { get; set; }

// ✅ بعد
public string TitleKey { get; set; } // "milestone.programming.fundamentals.title"
public string DescriptionKey { get; set; } // "milestone.programming.fundamentals.description"
```

### 7. MilestoneTask
```csharp
// ❌ قبل
public string TitleEn { get; set; }
public string TitleAr { get; set; }
public string DescriptionEn { get; set; }
public string DescriptionAr { get; set; }

// ✅ بعد
public string TitleKey { get; set; } // "task.learn.csharp.basics.title"
public string DescriptionKey { get; set; } // "task.learn.csharp.basics.description"
public bool IsOptional { get; set; } // ✨ جديد
```

### 8. TaskResource
```csharp
// ❌ قبل
public string TitleEn { get; set; }
public string TitleAr { get; set; }
public string? DescriptionEn { get; set; }
public string? DescriptionAr { get; set; }

// ✅ بعد
public string TitleKey { get; set; } // "resource.csharp.tutorial.title"
public string? DescriptionKey { get; set; } // "resource.csharp.tutorial.description"
```

### 9. Notification
```csharp
// ❌ قبل
public string TitleEn { get; set; }
public string TitleAr { get; set; }
public string MessageEn { get; set; }
public string MessageAr { get; set; }

// ✅ بعد
public string TitleKey { get; set; } // "notification.mentorship.accepted.title"
public string MessageKey { get; set; } // "notification.mentorship.accepted.message"
public string? MessageParams { get; set; } // ✨ JSON: {"mentorName": "Sara"}
```

---

## 📝 مثال على الاستخدام

### Backend (API Response)
```csharp
// Controller
var personalityType = new PersonalityType
{
    Code = "ANALYTICAL",
    NameKey = "personality.analytical.name",
    DescriptionKey = "personality.analytical.description"
};

// Response
{
  "code": "ANALYTICAL",
  "nameKey": "personality.analytical.name",
  "descriptionKey": "personality.analytical.description"
}
```

### Frontend (i18n Files)

**en.json:**
```json
{
  "personality": {
    "analytical": {
      "name": "Analytical Thinker",
      "description": "Logical, detail-oriented, problem solver"
    }
  },
  "track": {
    "backend": {
      "name": "Backend Development",
      "description": "Learn server-side development"
    }
  },
  "notification": {
    "mentorship": {
      "accepted": {
        "title": "Mentorship Request Accepted",
        "message": "{{mentorName}} accepted your mentorship request"
      }
    }
  }
}
```

**ar.json:**
```json
{
  "personality": {
    "analytical": {
      "name": "المفكر التحليلي",
      "description": "منطقي، دقيق، يحل المشاكل"
    }
  },
  "track": {
    "backend": {
      "name": "تطوير الباك إند",
      "description": "تعلم تطوير السيرفر"
    }
  },
  "notification": {
    "mentorship": {
      "accepted": {
        "title": "تم قبول طلب الإرشاد",
        "message": "{{mentorName}} قبل طلب الإرشاد الخاص بك"
      }
    }
  }
}
```

### Frontend (React/Next.js)
```typescript
import { useTranslation } from 'next-i18next';

function PersonalityTypeCard({ personalityType }) {
  const { t } = useTranslation();
  
  return (
    <div>
      <h3>{t(personalityType.nameKey)}</h3>
      <p>{t(personalityType.descriptionKey)}</p>
    </div>
  );
}

// Output (English):
// <h3>Analytical Thinker</h3>
// <p>Logical, detail-oriented, problem solver</p>

// Output (Arabic):
// <h3>المفكر التحليلي</h3>
// <p>منطقي، دقيق، يحل المشاكل</p>
```

### Notification مع Dynamic Values
```typescript
// Backend
var notification = new Notification
{
    TitleKey = "notification.mentorship.accepted.title",
    MessageKey = "notification.mentorship.accepted.message",
    MessageParams = JsonSerializer.Serialize(new { mentorName = "Sara" })
};

// Frontend
const params = JSON.parse(notification.messageParams);
const message = t(notification.messageKey, params);
// Output: "Sara accepted your mentorship request"
// Output (AR): "سارة قبلت طلب الإرشاد الخاص بك"
```

---

## 🔄 Migration Steps

### 1. إنشاء Migration
```bash
dotnet ef migrations add ConvertToI18nKeys
```

### 2. تعديل الـ Configurations
كل الـ configurations محتاجة تتعدل عشان تشيل الـ MaxLength من الـ En/Ar columns القديمة وتضيف للـ Keys الجديدة.

### 3. تحديث الـ Seed Data
```csharp
// مثال: PersonalityType Seed
new PersonalityType
{
    Code = "ANALYTICAL",
    NameKey = "personality.analytical.name",
    DescriptionKey = "personality.analytical.description",
    IsActive = true
}
```

### 4. Frontend i18n Setup
```bash
npm install next-i18next
# or
npm install react-i18next
```

---

## ✨ الفوائد

1. ✅ **Database أصغر** - مفيش تكرار للبيانات
2. ✅ **Scalable** - سهل تضيف لغات جديدة (French, Spanish, etc.)
3. ✅ **Maintainable** - كل الترجمات في مكان واحد
4. ✅ **Flexible** - Frontend يقدر يغير الترجمة بدون Backend
5. ✅ **Best Practice** - ده الـ standard في الـ modern apps
6. ✅ **Performance** - الترجمة بتحصل في الـ Frontend (مش Backend)

---

## 📊 قبل وبعد

### Database Size
```
قبل: 9 entities × 4 columns (En/Ar) = 36 text columns
بعد: 9 entities × 2 columns (Keys) = 18 text columns
توفير: 50% في الـ columns
```

### إضافة لغة جديدة
```
قبل: تعديل 9 entities + migration + seed data
بعد: إضافة ملف JSON واحد في الـ Frontend
```

---

## 🎯 Next Steps

1. ✅ تعديل الـ Configurations
2. ✅ إنشاء Migration
3. ✅ تحديث الـ Seed Data
4. ✅ تحديث الـ DTOs
5. ✅ Setup Frontend i18n
6. ✅ إنشاء ملفات الترجمة (en.json, ar.json)

---

**تم التحديث:** 8 نوفمبر 2025  
**الحالة:** ✅ مكتمل - جاهز للـ Migration
