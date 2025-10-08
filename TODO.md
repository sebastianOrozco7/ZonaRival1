# TODO - ZonaRival Frontend Updates

## Completed Tasks

### Modal de Desafío
- [x] Agregar modal de challenge después del modal de edit en Panel.cshtml
- [x] Modal con id="challengeModal", form method="post" action="/Equipo/CrearDesafio"
- [x] Campos: Modalidad (select: Fútbol 5, Fútbol 7, Fútbol 11), Cancha (select con canchas), Fecha (date)
- [x] Hidden: EquipoDesafiadoId (data-equipo-id), EquipoDesafianteId (Model.equipoViewModel.EquipoId)
- [x] Botones: Cancelar, Enviar Desafío
- [x] Mantener colores oscuros
- [x] Agregar JavaScript para setear equipoDesafiadoId al abrir modal

### Preview Updates
- [x] Actualizar preview-index.html con cambios similares
- [x] Agregar canchas en tarjetas de equipos (ej: Wembley, Caracoli)
- [x] Cambiar botones a modales con data-bs-toggle y data-equipo-id
- [x] Agregar modal de challenge después del edit modal
- [x] Agregar script para configurar modal de desafío

## Pending Tasks
- [ ] Implementar backend para CrearDesafio action en EquipoController
- [ ] Agregar validaciones en el formulario de desafío
- [ ] Probar funcionalidad completa del modal
- [ ] Actualizar estilos CSS si es necesario para modales oscuros
