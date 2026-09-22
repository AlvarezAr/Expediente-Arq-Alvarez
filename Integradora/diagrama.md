# Diagrama de clases — Comedor Universitario "Sabor Andino"
---
## Estudiante: Ariel Alvarez Mamani

## Parte 1 — El plano (6 puntos)

diagrama.md (Mermaid) o imagen legible: el diagrama de clases del sistema a partir de los requerimientos (receta
de 4 pasos: sustantivos, verbos, filtro, relaciones). Con tu nombre completo visible dentro del diagrama.

```mermaid
classDiagram

    Estudiante --> Pedido
    Cajero --> Pedido
    Administrador --> Pedido
    Pedido --> Menu
    Pedido --> EstadoPedido
    Menu --> TipoMenu
    Administrador --> ReporteVentas

    Pedido --> IObservadorPedido
    Estudiante ..|> IObservadorPedido
    IObservadorPedido --> AvisoPedido

    class Estudiante{
        +String nombre
        +String carnet
        +realizarPedido()
        +recibirAviso()
    }

    class Pedido{
        +int id
        +int cantidad
        +decimal total
        +EstadoPedido estado
        +registrar()
        +preparar()
        +entregar()
        +anular()
        +notificar()
    }

    class Menu{
        +TipoMenu tipo
        +decimal precio
    }

    class Cajero{
        +String nombre
        +registrarPedido()
    }

    class Administrador{
        +String nombre
        +ajustarPrecio()
        +anularPedido()
        +generarReporte()
    }

    class ReporteVentas{
        +String periodo
        +generarPorTipo()
    }

    class AvisoPedido{
        +enviar()
    }

    class IObservadorPedido{
        <<interface>>
        +actualizar()
    }

    class EstadoPedido{
        <<enumeration>>
        SOLICITADO
        PREPARADO
        ENTREGADO
        ANULADO
    }

    class TipoMenu{
        <<enumeration>>
        ESTANDAR
        VEGETARIANO
        BECA
    }
```
---