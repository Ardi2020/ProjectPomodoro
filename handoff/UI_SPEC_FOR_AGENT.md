# Project Pomodoro — UI Implementation Spec (Modernist)

Instruksi untuk coding agent / developer. Target: repo `Ardi2020/ProjectPomodoro` (C# .NET 10, WPF, Windows 11).
Referensi visual: `reference-mainwindow.png` (MainWindow) dan `reference-focus-break.png` (FocusWindow full-screen, mini bar, BreakWindow).
JANGAN mengubah logika di code-behind (LocalStore, session flow, ProgressText) — ini murni restyling + penataan layout. Semua nama event handler yang ada (`NewProject_Click`, `AddTask_Click`, `StartFocus_Click`, dst.) tetap dipakai.

## 0. Design system "Modernist" — aturan mutlak
- Font: **Archivo** (embed TTF), fallback Segoe UI. Heading/angka timer: ExtraBold (800).
- **Radius 0 di semua elemen.** Tidak ada sudut membulat.
- Struktur digambar dengan garis: **2px `#66201E1D`** antar section besar, **1px `#26201E1D`** antar baris task.
- Satu aksen: merah `#EC3013`. Selain tombol utama, tag prioritas, dan progress — semuanya tinta (`#201E1D`) di atas ground (`#F3F2F2`).
- Label tombol **rata kiri** (`HorizontalContentAlignment=Left`), tidak pernah center.
- Tidak ada emoji; estimasi pomodoro digambar sebagai kotak 8×8 px (terisi = selesai, outline = sisa).
- Semua warna & style diambil dari `Theme.xaml` (sudah disediakan) — jangan hard-code brush baru.

## 1. Pemasangan
1. Salin `Theme.xaml` → `ProjectPomodoro/Themes/Theme.xaml`.
2. Salin `app.ico` → `ProjectPomodoro/Assets/app.ico`.
3. Unduh font Archivo (Google Fonts, weight 400–800) → `ProjectPomodoro/Assets/Fonts/Archivo-*.ttf`, Build Action `Resource`.
4. `App.xaml`:
   ```xml
   <Application.Resources>
     <ResourceDictionary>
       <ResourceDictionary.MergedDictionaries>
         <ResourceDictionary Source="Themes/Theme.xaml"/>
       </ResourceDictionary.MergedDictionaries>
     </ResourceDictionary>
   </Application.Resources>
   ```
5. `ProjectPomodoro.csproj`: `<ApplicationIcon>Assets\app.ico</ApplicationIcon>`; pada setiap Window set `Icon="/Assets/app.ico"`.
6. Style lama `ActionButton` → ganti/alias ke `PrimaryButton`; `QuietButton` → `SecondaryButton` atau `GhostButton` (lihat pemetaan di bawah).

## 2. MainWindow (lihat reference-mainwindow.png)
Grid 3 kolom: **260 | * | 290**, background `{Bg}`, dipisah border kanan/kiri 2px `{Divider}`.

### 2.1 Title bar
Tinggi 38, `{Bg}`, border bawah 2px: kotak merah 14×14 + "PROJECT POMODORO" (11px, ExtraBold, letter-spacing lebar). Boleh pakai chrome Windows standar bila custom chrome terlalu mahal.

### 2.2 Sidebar (kolom 1)
- Header "PROJECTS" (Kicker) + jumlah, border bawah 2px.
- Item project (ItemsControl, ganti ListBox default): judul (14px Bold), `ProgressBar` FlatProgress (nilai = DoneLeafCount/LeafCount), teks `ProgressText` (11px `{TextMuted}`). Border bawah 1px `{DividerSoft}`.
  - Terpilih: background `{AccentTint100}` + border kiri 4px `{Accent}`.
  - Hover: background `{Surface}`.
- "+ New project": teks merah Bold 13px, hover `{AccentTint100}` (GhostButton dengan Foreground `{Accent}`).
- Paling bawah (dock): "Settings", border atas 2px.

### 2.3 Header project (kolom 2, atas)
Border bawah 2px. Kiri: judul (H1 26px) + tag status ("IN PROGRESS": background `{AccentTint200}`, teks `{AccentDeep}`, 10px ExtraBold; "NOT STARTED": outline 1px `{TextFaint}`); di bawahnya progress bar 180×6 + `ProgressText`.
Kanan (StackPanel horizontal, gap 8): `+ Task` (PrimaryButton), `+ Milestone` (SecondaryButton), `History` (GhostButton), `⋯` (GhostButton berisi menu: Mark complete, Archive, Settings). **Mark complete & Archive pindah ke menu ⋯** — jangan sederet dengan aksi utama.

### 2.4 Daftar milestone + task (kolom 2, scroll)
- **Header milestone**: border atas 2px `{Divider}`; isi: "MILESTONE 01" (10px ExtraBold `{Accent}`; abu `{TextFaint}` jika belum ada task berjalan) + judul (14px Bold) + "x/y selesai" + tag deadline outline ("DUE 30 SEP 2026"). Tampilkan task ber-`MilestoneId` di bawah headernya; task tanpa milestone di grup "TASKS" di akhir.
- **Baris task** (ganti kartu WhiteSmoke lama): border atas 1px `{DividerSoft}`, padding 12/24.
  - Checkbox kotak 16×16 border 2px (Done: terisi `{Ink}` + centang putih; klik = `ToggleDone_Click`).
  - Judul 14px (Done: `{TextFaint}` + strikethrough). Tag "HIGH" merah solid 9px bila prioritas tinggi. Baris "Why:" 11px `{TextMuted}` bila ada.
  - Kanan (lebar tetap agar rata antar baris): kotak-kotak pomodoro 8×8 (sesi selesai = terisi; estimasi sisanya outline), teks "2/4 · 52 min" (11px, lebar 86), tombol "Start 25 min" (lebar 96; PrimaryButton pada task aktif/in-progress, SecondaryButton lainnya; Done: "Reopen" GhostButton).
  - Task in-progress: background baris `{AccentTint100}`.
  - Subtask: indentasi kiri 28px, checkbox 14px, font 13px, tanpa tombol +Subtask.
- Tambahkan properti `Priority` (enum Low/Normal/High) dan `EstimatedPomodoros:int` pada `WorkItem`, serta `DueDate:DateTimeOffset?` pada `Milestone` — dipakai tag HIGH, kotak estimasi, tag DUE.

### 2.5 Panel Focus Session (kolom 3)
- Header "FOCUS SESSION" (Kicker), border bawah 2px.
- Timer: angka 56px ExtraBold tabular (`Typography.NumeralAlignment=Tabular`), progress bar 8px, nama task Bold 13px + "Pomodoro n · <project>" 11px, tombol Pause (isi `{Ink}`) + Stop (SecondaryButton), kutipan FocusMessages 11px italic di bawah garis 1px. Saat idle: teks "Tidak ada sesi aktif" + petunjuk "Pilih task lalu Start 25 min".
- Dock bawah "TODAY" (border atas 2px): 3 baris label–nilai (Pomodoro selesai / Waktu fokus / Task selesai) dihitung dari `store.Data.Sessions` hari ini; tiap baris border atas 1px.

## 3. FocusWindow — full-screen merah (reference-focus-break.png atas)
`WindowState=Maximized`, `WindowStyle=None`, background `{Accent}`.
- Atas: kotak putih 12×12 + "FOCUS · POMODORO n OF m" (11px ExtraBold `#FFC4B8`); kanan: "MINIMIZE" → menyusut jadi mini bar.
- Tengah (rata kiri, margin 40): `RemainingText` 110px ExtraBold `#F8F4F4` tabular; progress bar 8px (track `#AE1800`, fill putih, max 420); `TitleText` 20px Bold; `WhyText` 13px `#FFC4B8`; `MotivationText` 13px italic `#FFE0D9`.
- Bawah: "Pause" (putih, teks merah) + "Stop early" (outline putih 2px). Stop tetap memicu dialog konfirmasi yang ada.

## 4. Mini bar floating (mode minimize FocusWindow)
Window terpisah/ukuran kecil: 400×56, `Topmost=True`, `WindowStyle=None`, `ResizeMode=NoResize`, background `{Ink}`, bisa digeser (`DragMove` pada MouseLeftButtonDown), posisi tersimpan.
Isi: angka 26px putih tabular | nama task 10px `#9B9797` uppercase + progress 4px (track `#444141`, fill `{Accent}`) | tombol pause (merah solid) + stop (outline `#605D5D`). Klik area teks = kembalikan full-screen.

## 5. BreakWindow (reference-focus-break.png bawah)
400×64, `Topmost=True`, background `#F8F4F4`, border 2px `{Ink}`: angka 26px `{Ink}` | "BREAK · 5 MIN" 10px ExtraBold `{TextMuted}` + progress 4px (fill `{Ink}`, **bukan merah** — merah hanya untuk fokus) | tombol "Skip" outline 2px.

## 6. Dialog pengganti MessageBox/Prompt
Semua `MessageBox`/`Prompt` diganti Window flat: background `{Bg}`, border 2px `{Ink}`, tanpa radius; judul Kicker; isi 13px; tombol rata kiri di baris bawah (primary merah + secondary outline). Yang wajib diganti: New project / Add task / Add milestone / Add subtask (TextBox flat border 2px `{Ink}`, fokus border `{Accent}`), "What next?" (3 tombol: Mark done / Take a break / Start another), pilihan durasi break (segmented: 5 / 10 / custom), konfirmasi Stop, History (window dengan tabel: header Kicker, baris border 1px).

## 7. Checklist penerimaan
- [ ] Tidak ada sudut membulat & tidak ada warna di luar Theme.xaml
- [ ] Semua label tombol rata kiri
- [ ] Garis 2px antar section, 1px antar baris
- [ ] Timer pakai angka tabular (tidak bergetar saat berubah)
- [ ] Mark complete/Archive di menu ⋯, tidak sederet aksi utama
- [ ] Icon app.ico tampil di title bar + taskbar
- [ ] Perilaku existing tidak berubah (session, save/load, progress leaves)
