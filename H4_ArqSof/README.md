# C4 COMERCIO — "Tienda de Tenis con inventario"
---
## Nivel 1 — Contexto (el sistema y su mundo)

La pregunta que responde: **¿quién usa el sistema y con qué otros sistemas habla?**
Una caja para TU sistema; personas y sistemas externos alrededor. Nada de detalles internos.

```mermaid
flowchart TB
    admin["👤 Administrador<br>(+ Gestionar Productos <br> + Generar Reportes)"]
    cajero["👤 Cajero<br>(+ Gestionar Ventas<br>+ Emitir Factura<br>+ Consultar Inventario)"]
    cliente["👤 Cliente<br>(recibe avisos de su compra)"]
    sistema["👟 SISTEMA DE TIENDA DE TENIS CON INVENTARIO<br>Gestiona Ventas, Gestiona Productos<br>y Notifica cuando algo esta con Inventario Minimo"]
    Notificacion["📧 Servicio de Notificacion whatsapp - Telegram <br>(externo)"]
    Pagos["💳 Pagos Qr BNB - Banco Sol<br>(externa)"]
    SIAT["🧾Emicion de Factura<br>(externa)"]
    cajero -->|"Gestiona Productos y Inventarios"| sistema
    admin -->|"Gestiona Ventas, Emite Factura"| sistema
    Notificacion -->|"Notifica Venta"| cliente
    sistema -->|"envía comprobantes y avisos"| Notificacion
    Notificacion -->|"Notifica Venta"| admin
    sistema -->|"cobra en Qr"| Pagos
    sistema -->|"Valida su Nit"| SIAT
```
---

## Nivel 2 — Contenedores (el zoom adentro del sistema)

