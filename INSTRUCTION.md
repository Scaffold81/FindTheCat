# FindTheCat — Инструкция по сборке и оптимизации размера

---

## 1. Установка пакетов (Package Manager)

### Zenject (Extenject)
1. `Window → Package Manager → + → Add package by name`
2. Вставить: `com.svermeulen.extenject`

### R3
1. Открыть `Packages/manifest.json`
2. Добавить scopedRegistry и зависимости:

```json
{
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com",
      "scopes": [ "com.cysharp" ]
    }
  ],
  "dependencies": {
    "com.cysharp.r3": "1.2.7",
    "com.cysharp.unitask": "2.5.10"
  }
}
```

---

## 2. Настройка сцены (порядок действий)

### ProjectContext
```
GameObject → Zenject → Project Context
```
- В **Scriptable Object Installers** → добавить `SearchableConfigInstaller`
- Назначить `SearchableConfig.asset` в поле `_config`

### SceneContext
```
GameObject → Zenject → Scene Context
```
- В **Mono Installers** → добавить `SearchableInstaller`

### Создать конфиг
```
ПКМ в Project → Create → FindTheCat → SearchableConfig
```
Сохранить в `Assets/Settings/SearchableConfig.asset`

| Type | MaxCount |
|------|----------|
| Cat  | 5        |

### Объект-кот на сцене
1. Создать `GameObject → Sprite`
2. Добавить компонент `CatObject`
3. Добавить `PolygonCollider2D` или `BoxCollider2D`
4. В поле `_type` выбрать `Cat`
5. В поле `_foundColor` задать цвет (например жёлтый)

### UI счётчик
```
Иерархия:
Canvas
 └── SearchableView  [SearchableView.cs]
      └── CatBadge   [Image — жёлтый фон]
           ├── Icon  [Image — спрайт кота]
           └── Label [TMP_Text — "0/5"]
```
В инспекторе `SearchableView → Entries [0]`:
- Type  = `Cat`
- Icon  = ссылка на `Image (Icon)`
- Label = ссылка на `TMP_Text (Label)`

---

## 3. Оптимизация размера — цель до 20 МБ

### 3.1 Build Settings
```
File → Build Settings → WebGL
```

| Параметр | Значение |
|----------|----------|
| Compression Format | **Gzip** |
| Code Optimization | **Size** |
| Strip Engine Code | ✅ |
| Managed Stripping Level | **High** |
| Exception Support | **None** (для релиза) |

### 3.2 Player Settings → Other Settings

| Параметр | Значение |
|----------|----------|
| Scripting Backend | **IL2CPP** |
| Api Compatibility Level | **.NET Standard 2.1** |
| Strip Engine Code | ✅ |
| Vertex Compression | Position, Normal, Color, TexCoord |

### 3.3 Player Settings → Publishing Settings (WebGL)

| Параметр | Значение |
|----------|----------|
| Compression Format | **Gzip** |
| Data Caching | ✅ |
| Linker Target | **WASM** |

### 3.4 Текстуры

| Параметр | Значение |
|----------|----------|
| Format | **WebP** или **ETC2** |
| Max Size | не больше 512×512 для UI, 1024×1024 для фона |
| Compression | **Normal Quality** |
| Generate Mip Maps | ❌ (для 2D/UI всегда выключать) |
| Crunch Compression | ✅ Quality 50–70 |

### 3.5 Аудио

| Параметр | Значение |
|----------|----------|
| Format | **Vorbis** |
| Quality | 40–60% |
| Load Type | **Compressed In Memory** (SFX) |
| Load Type | **Streaming** (фоновая музыка) |
| Частота | 22050 Hz (не 44100) |

### 3.6 Шрифты
- Использовать **TMP Font Asset** с урезанным набором символов
- `Window → TextMeshPro → Font Asset Creator`
- В поле **Character Set** выбрать `Custom Characters`
- Вписать только нужные символы: `0123456789/Осталькотв`
- Размер атласа: **256×256** или **512×512**

### 3.7 Что удалить из проекта
- `TutorialInfo` — удалить папку
- `InputSystem_Actions.inputactions` — если не используется Input System
- Все неиспользуемые пакеты из `manifest.json`
- `Readme.asset` — удалить

### 3.8 Удалить лишние модули Unity
```
Unity Hub → Installs → Unity 6 → Add Modules
```
Убрать: Android, iOS, tvOS, WebGL (оставить только нужные платформы)

---

## 4. Интеграция Яндекс.Игры

### 4.1 Подключить SDK
Скачать последнюю версию: https://github.com/yandex-games-sdk/unity

Положить в: `Assets/Plugins/YandexGames/`

### 4.2 index.html
WebGL шаблон — `Assets/WebGLTemplates/YandexGames/index.html`:

```html
<script src="/sdk.js"></script>
<script>
  YaGames.init().then(ysdk => {
    window.ysdk = ysdk;
  });
</script>
```

### 4.3 Player Settings → WebGL
```
Resolution and Presentation → WebGL Template → YandexGames
```

### 4.4 Инициализация в Unity
```csharp
// Вызвать до показа любого контента
YandexGamesSdk.Initialize();
```

### 4.5 Реклама
```csharp
// Межстраничная (между уровнями)
InterstitialAd.Show(onOpenCallback, onCloseCallback, onErrorCallback);

// Rewarded (за подсказку)
VideoAd.Show(onOpenCallback, onRewardedCallback, onCloseCallback, onErrorCallback);
```

---

## 5. Чеклист перед публикацией

- [ ] Размер сборки ≤ 20 МБ (проверить в папке Build)
- [ ] Compression = Gzip
- [ ] `ysdk.features.LoadingAPI.ready()` вызывается после инициализации
- [ ] Игра работает на мобильном (Canvas Scaler = Scale With Screen Size)
- [ ] Нет Console Errors в WebGL
- [ ] `Application.targetFrameRate = 60`
- [ ] Все текстуры сжаты
- [ ] Аудио Vorbis 40–60%
- [ ] TMP шрифт с урезанным символьным набором

---

## 6. Как проверить размер до сборки

```
Window → Analysis → Build Report
```
Показывает размер каждого ассета — видно что занимает больше всего места.

Либо после сборки смотреть файл:
```
Build/
 ├── Build.data.gz       ← основные ассеты
 ├── Build.wasm.gz       ← код
 └── Build.framework.js.gz
```
Сумма всех `.gz` файлов = реальный размер загрузки.
