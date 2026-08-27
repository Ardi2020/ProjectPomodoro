# Cara pakai paket handoff ini

Isi folder `handoff/`:
- `UI_SPEC_FOR_AGENT.md` — instruksi lengkap untuk agent/developer
- `Theme.xaml` — resource dictionary warna, font, dan style tombol
- `app.ico` — icon aplikasi (256/64/48/32/16 px), `icon-256.png` versi PNG
- `reference-mainwindow.png`, `reference-focus-break.png` — gambar acuan desain

## Penempatan di repo ProjectPomodoro
```
ProjectPomodoro/
├── Assets/
│   ├── app.ico              ← dari handoff
│   └── Fonts/Archivo-*.ttf  ← unduh dari fonts.google.com/specimen/Archivo
├── Themes/
│   └── Theme.xaml           ← dari handoff
├── MainWindow.xaml / .cs
├── FocusWindow.xaml / .cs
└── BreakWindow.xaml / .cs
```
`UI_SPEC_FOR_AGENT.md` + kedua PNG tidak ikut di-build; taruh di root repo (mis. folder `design/`) supaya bisa dibaca agent.

## Menjalankan dengan agent di VS Code
1. Salin seluruh folder `handoff/` ke root repo sebagai `design/`.
2. Buka repo di VS Code, jalankan agent (Copilot/Claude), beri prompt:
   > Implementasikan restyling UI sesuai `design/UI_SPEC_FOR_AGENT.md`. Gunakan `design/Theme.xaml` dan `design/app.ico` (pindahkan ke lokasi yang disebut spec). Cocokkan hasil dengan `design/reference-mainwindow.png` dan `design/reference-focus-break.png`. Jangan mengubah logika domain/penyimpanan.
3. Build & jalankan: `dotnet run --project ProjectPomodoro`.
4. Bandingkan hasil dengan PNG acuan; minta agent perbaiki selisihnya.
