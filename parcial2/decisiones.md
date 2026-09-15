# P2.1 — Decisiones ELEGUIDAS Y SU JUSTIFICACION
## Situación 1
Patrón a Aplicar: Observer

PORQUE LO APLICARIA: Por que a diferencia de otros patrones observer puede notificar o en pocas palabras avisar a sus interesados sin necesidad de conocerlos y a su vez el nuevo modulos de promociones seria como un nuevo suscriptor al cual le notificaremos

PORQUE NO OTRO: Porque es tipo notificador a todos los interesados, y si va creciendo en un futuro se implementa a sus nuevos interesados de las notificaciones, ya sea de promocion, de finalizacion de su membresia u otro motivo.

## Situación 2
Patrón a Aplicar: Strategy

PORQUE LO APLICARIA: yo lo aplicaria porque es lo ideal para evitarnos los if, elseif eternos por cada tipo de tarifario ya sea de dia de noche o en fines de semana, en la cual este patron separa cada regla de calculo en una estrategia independiente a las demas.

PORQUE NO OTRO: porque ninguo se adapta a separar individualmente cada cotizacion del gimnacio y a sus interesados que tiene diferentes tipos de necesidades y me ahorraria el hacer muchos if en el codigo.

## Situación 3
Patrón a Aplicar: Adapter

PORQUE LO APLICARIA: porque es el ideal cuando hablamos de un traductor en la frontera con lo ajeno en este contexto seria el SDK externo y poder traducir sus token y demas en bs., que es lo que le venificiaria al dueño del gimnacio.

PORQUE NO OTRO: es el mas indicado a conversiones ya que vienen con la instruccion que no es modificable, entoces tendriamos que aplicar un traductor para el cobro en bs dependiendo la tarifa a aplicarse al cliente.

# P2.3 — SOLID - implementacion de P2.2




