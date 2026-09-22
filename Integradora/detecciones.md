# Detección de violaciones SOLID— Comedor Universitario "Sabor Andino"
---
## Estudiante: Ariel Alvarez Mamani

## Parte 2 — La cirugía SOLID (8 puntos)

El archivo `esqueleto-A.cs` adjunto tiene `3 violaciones SOLID`.
En detecciones.md : las 3 identificadas (principio, dónde, por qué — 1-2 líneas c/u). (4.5)

## 1. (S) - SINGLE RESPONSABILITY PRINCIPLE
---
**Dónde:** `GestorDePedidos.ProcesarPedido()`.
---
**Por qué:** La clase `GestorDePedidos` realiza demasiadas responsabilidades: calcula el precio del menú, calcula el total, guarda el pedido en la base de datos, imprime el vale del comedor y envía el correo. En la cual tiene múltiples responsabilidades y esto llegaria a correguirse con (S) - SINGLE RESPONSABILITY PRINCIPLE.
---
### 2. (O) — OPEN / CLOSED PRINCIPLE
---
**Dónde:** `switch (tipoMenu)` dentro de `GestorDePedidos.ProcesarPedido()`.
---
**Por qué:** Para agregar un nuevo tipo de menú o modificar su precio es necesario modificar el método de la clase existente. En la cual si agregariasmos al menú `"sin_lactosa"`, estaria obligado a editar el `switch (tipoMenu)`, por lo que el código no está abierto a extensión y cerrado a modificación, como lo exige este principio.
---
### 3. (D) — DEPENDENCY INVERSION PRINCIPLE
---
**Dónde:** En `GestorDePedidos.ProcesarPedido()`
---
**Por qué:** `GestorDePedidos` depende directamente de las clases concretas `BaseDeDatosComedor` y `CorreoUniversitario`. En lugar de depender de abstracciones, crea y controla directamente las implementaciones concretas, aumentando el acoplamiento y dificultando su sustitución o prueba.
