export function saveBytes(fileName, contentType, bytes) {
    const blob = new Blob([bytes], { type: contentType || "application/octet-stream" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");
    link.href = url;
    link.download = fileName || "riport";
    link.style.display = "none";
    document.body.appendChild(link);
    link.click();
    link.remove();

    // Hagyjuk, hogy a böngésző elindítsa a letöltést az URL visszavonása előtt.
    window.setTimeout(() => URL.revokeObjectURL(url), 1000);
}
