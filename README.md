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

---

## 📂 Project Structure

The project organizes files and folders to keep uploads and converted files separate. Folders are automatically created at runtime.

```bash
wwwroot/
 ├─ Uploads/       # Stores uploaded files temporarily
 └─ Converted/     # Stores converted output files

Controllers/
 └─ HomeController.cs  # Handles file upload, conversion, and download

Views/
 └─ Home/
     └─ Convert.cshtml               # Razor view for file upload and conversion selection
```
Folders are created automatically at runtime.

---

## 🚀 How It Works

- User uploads a file (.docx)
- File is saved to wwwroot/Uploads
- LibreOffice runs in headless mode
- Converted file is saved to wwwroot/Converted
- Browser automatically downloads the result

---

## 🔁 Supported Conversions
| Conversion | Status          |
| ---------- | --------------- |
| DOCX → PDF | ✅ Supported     |
| PDF → DOCX | ❌ Not supported |

---

## ❌ Why PDF → DOCX Is Not Supported

LibreOffice cannot export PDFs to DOCX via CLI.

PDF files are opened as Draw documents, not Writer documents, and LibreOffice does not provide an export filter for PDF → DOCX.

Error you may see:

```pgsql
Error: no export filter found
```

---

## ▶️ Running the Project

```cmd
dotnet restore
dotnet run
```

Open in browser:

```cmd
https://localhost:xxxx/SimpleConversion
```

---

## 📚 Technologies Used

- ASP.NET Core MVC (.NET 8)
- LibreOffice CLI (soffice)
- Razor Views
- C#

---
