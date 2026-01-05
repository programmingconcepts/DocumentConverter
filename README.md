# ASP.NET Core Document Converter (.NET 8)

A simple **ASP.NET Core MVC (.NET 8)** tutorial project that demonstrates how to convert documents using **LibreOffice (CLI)**.

✅ DOCX → PDF  
❌ PDF → DOCX (not supported by LibreOffice CLI – explained below)

This project is intended for **learning and demonstration purposes only**, not production use.

---

## 📌 Features

- ASP.NET Core MVC (.NET 8)
- File upload via Razor View
- Document conversion using `soffice` (LibreOffice CLI)
- Automatic download of converted file
- Minimal code (everything inside controller)
- Windows-based solution

---

## 🛠️ Requirements

- **Windows OS**
- **.NET 8 SDK**
- **LibreOffice (installed system-wide)**  
  👉 https://www.libreoffice.org/download/download/

Make sure `soffice.exe` is available in your system **PATH**.

Verify:
```cmd
soffice --version
```
