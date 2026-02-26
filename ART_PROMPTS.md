# Промпты для генерации графики — FindTheCat (Яндекс.Игры)

Стиль всей игры: **2D, мультяшный, яркий, детский, flat-art**.
Фон белый или прозрачный если возможно. Без теней. Без текста на изображениях.

---

## 🐱 Кот (главный объект поиска)

### Кот — обычное состояние
```
Cute cartoon cat character, sitting pose, chubby round body, big eyes, 
orange tabby cat with white belly, flat 2D art style, bright colors, 
white background, no shadows, no text, game asset style, 
isolated character, front view, chibi proportions
```

### Кот — найден (радость)
```
Cute cartoon cat character, happy expression, sitting pose, raising paws up, 
orange tabby cat, flat 2D art style, bright yellow glow around body, 
sparkles around, white background, no shadows, game asset style, 
isolated character, chibi proportions, celebrating pose
```

### Иконка кота для UI бейджа
```
Small cute cartoon cat face icon, round shape, orange cat, big eyes, 
flat 2D art style, suitable for UI badge, white background, 
simple design, no text, game icon style, 512x512
```

---

## 🐰 Заяц (дополнительный объект)

### Заяц — обычное состояние
```
Cute cartoon rabbit character, sitting pose, chubby round body, big eyes, 
white fluffy rabbit with pink ears, flat 2D art style, bright colors, 
white background, no shadows, no text, game asset style, 
isolated character, front view, chibi proportions
```

### Иконка зайца для UI бейджа
```
Small cute cartoon rabbit face icon, round shape, white rabbit, pink ears, 
big blue eyes, flat 2D art style, suitable for UI badge, white background, 
simple design, no text, game icon style, 512x512
```

---

## 🏠 Фоны (игровые уровни)

### Уровень 1 — Уютная комната
```
Cozy cartoon living room background, top-down slightly angled view, 
bright colors, flat 2D art style, sofa, bookshelf, carpet, curtains, 
plants, coffee table, many hiding spots for a cat, 
no characters, no text, game background, 1920x1080, 
cheerful children game style
```

### Уровень 2 — Сад / Двор
```
Cartoon sunny garden background, flat 2D art style, bright colors, 
flowers, bushes, trees, garden fence, bird bath, flower pots, 
many hiding spots, no characters, no text, 
children game background, 1920x1080, top-down view
```

### Уровень 3 — Кухня
```
Cute cartoon kitchen background, flat 2D art style, bright colors, 
cabinets, fridge, table, chairs, fruit bowl, many hiding spots for cat,
no characters, no text, children game background, 1920x1080
```

---

## 🎨 UI Элементы

### Фон для бейджа счётчика (как на скриншоте)
```
Small rounded rectangle badge UI element, warm yellow-orange gradient, 
flat 2D game UI style, glossy finish, no text, no icons, 
suitable for game HUD counter, white background, 
game UI asset, 256x64 proportions
```

### Кнопка Play / Играть
```
Cartoon game button, green color, rounded rectangle shape, 
flat 2D style, glossy, bright, no text, game UI element, 
white background, isolated, clean design
```

### Кнопка Pause
```
Cartoon game pause button, circular shape, soft blue color, 
two vertical bars icon inside, flat 2D style, game UI element, 
white background, isolated, no text
```

### Кнопка Hint / Подсказка (лампочка)
```
Cute cartoon hint button, circular yellow button, 
lightbulb icon in center, flat 2D style, bright yellow glow, 
game UI element, white background, isolated, no text
```

### Звезда для результата
```
Cartoon gold star, 5-pointed, shiny, flat 2D style, 
bright yellow with orange outline, sparkles around, 
game reward element, white background, isolated, no shadows
```

### Частицы / Конфетти (для анимации находки)
```
Colorful confetti explosion, small pieces, stars, circles, 
bright rainbow colors, flat 2D style, transparent background, 
celebration effect, game particle sprite sheet, 
4 frames animation strip
```

---

## 🌟 Главное меню

### Логотип / Заголовок игры
```
Cute cartoon game logo illustration, cat hiding behind text area, 
peeking cat with big eyes, magnifying glass element, 
flat 2D style, bright colors, orange and yellow palette, 
no actual text, just decorative frame and cat character, 
children game style
```

### Фон главного меню
```
Cute cartoon background for children mobile game main menu, 
cozy room with hidden cats peeking from corners, 
flat 2D art style, soft warm colors, 
bokeh blurred background effect, no text, 1080x1920
```

---

## ⚙️ Технические требования для всех промптов

Добавлять в конце каждого промпта:
```
PNG format, transparent background, no watermarks, 
2D flat game asset, children mobile game style
```

---

## 💡 Советы по генерации

- Если кот получается слишком реалистичным — добавить: `more cartoonish, chibi style, exaggerated features`
- Если фон слишком детализирован — добавить: `simple, flat colors, minimal details`
- Для одинакового стиля всех ассетов — начинать каждый промпт с: `In the same flat 2D cartoon game art style as previous image,`
- Размеры для Unity: кот и заяц — **512×512**, фоны — **1920×1080**, UI иконки — **256×256**
