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

La pregunta que responde: **¿de qué piezas ejecutables/almacenes está hecho el sistema?**
Cada contenedor es algo que corre o almacena: la app web, la base de datos, un servicio.

```mermaid
flowchart TB
    cajero["👤 Cajero"]
    admin["👤 Administrador"]
    subgraph sistema["👟 SISTEMA DE TIENDA DE <br> TENIS CON INVENTARIO"]
        webapp["🌐 Aplicación web<br>C# / ASP.NET<br>Pantallas de Gestion de Productos, Gestion de Venta, Inventarios y Reportes"]
        api["⚙️ Lógica de negocio<br/>C#<br/>Gestión de ventas, productos e inventario<br/>Cálculo de precios y descuentos"]
        bd[("🗄️ Base de datos || bdTiendaTenis<br/>SQL / MySQL<br/>Productos, categorías, proveedores,<br/>ventas, detalles e inventario")]
        Notificacion["🤳 Servicio de Notificacion<br>C#<br>Observer: Notifica las Ventas Realizadas Exitosamente<br>al Adminstrador y al cliente"]
        Convertidor["💶 Servicio de Convertidor<br>C#<br>Adapter: Convierte el precio de los productos desde su procedencia a bs con un margen de ganancia y impuestos de ley<br>a los proveedores de Asia y Peru"]
    end
    Pagos["💳 Pagos Qr BNB - Banco Sol<br>(externa)"]
    SIAT["🧾Emicion de Factura<br>(externa)"]
    whatsapp["📧 Servicio de Notificacion whatsapp  (externo)"]
    Telegram["📧 Servicio de Notificacion Telegram  (externo)"]
    Sol["💶 Servicio de Convertidor de Soies a Bs.  (externo)"]
    Yuan["💷 Servicio de Convertidor de Yuanes a Bs.  (externo)"]
    cajero --> webapp
    admin --> webapp
    webapp --> api
    api --> bd
    api -->|"publica evento Notificador a los Interesados de las Ventas efectuadas"| Notificacion
    api -->|"publica evento Conversion de Moneda extranjera a Bs."| Convertidor
    api -->|"cobra en Qr"| Pagos
    api -->|"Valida su Nit"| SIAT
    Notificacion --> whatsapp
    Notificacion --> Telegram
    Convertidor --> Sol
    Convertidor --> Yuan
```