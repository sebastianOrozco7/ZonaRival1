# TODO - ZonaRival Frontend Updates (Actualización Nueva Tarea)

## Completed Tasks (from previous)
- [x] Modal de Desafío en Panel.cshtml y preview-index.html (form, campos, JS config ID).
- [x] Botones "Desafiar" con clase challenge-btn (naranja atractivo).
- [x] Creación inicial TODO.md.

## New Task Plan (Incorporando Feedback Usuario)

### Información Gathered
- Formulario registro: Angosto (contenedor fijo ~400px), no fully responsive; campos en column, necesita grid/flex para móviles.
- Modales (edit/challenge): Básicos Bootstrap; mejorar con dark theme, gradientes (azul para edit, naranja para challenge), blur, sombras profesionales.
- Validación contraseña: JS para ≥8 chars + 1 especial (!@#$%^&*); alert/error message on submit.
- Botón disponibilidad: Actual green para "Habilitar"; conditional red para "Quitar/Deshabilitar" (nuevo CSS .disable-btn con gradiente rojo).
- Nuevo section "Encuentros pendientes": Cards como available-teams; ejemplos estáticos (3 equipos desafiantes con info, teléfono hidden); botones Aceptar (green, post /Equipo/AceptarDesafio/{id}, toggle show teléfono via JS), Rechazar (red, post /Equipo/RechazarDesafio/{id}).

### Plan Detallado
- [] wwwroot/css/registro.css: Ajustar .form-container (width: 90vw max 500px), .form (flex/grid responsive), media queries para móviles (stack fields, reduce padding).
- [] Views/Inicio/registro.cshtml y wwwroot/preview-registro.html: Añadir clases responsive, JS validarPassword() on submit (check length/special, prevent submit if invalid, show error div).
- [] wwwroot/css/index.css: Añadir .modal-custom (backdrop rgba(0,0,0,0.8), .modal-content rgba(13,20,40,0.95) blur(10px), headers gradiente azul/naranja, inputs themed dark, buttons styled). .disable-btn (gradiente rojo #e53e3e to #c53030, hover dark red). .pending-section (similar available-teams, cards with accept/reject buttons).
- [] Views/Home/Panel.cshtml y wwwroot/preview-index.html: 
  - Añadir <input type="time" name="Hora" required> después de Fecha en challenge modal.
  - Añadir class="modal-custom" a #editModal y #challengeModal.
  - Botón disponibilidad: Conditional class (@if Disponibilidad { "disable-btn" } else { "availability-btn" }), text "Quitar Disponibilidad" / "Habilitar Disponibilidad".
  - Nuevo <div class="section pending-section"> después de historial: <h2>Encuentros Pendientes</h2>, <div class="pending-teams"> con 3 example cards (info equipo, teléfono hidden id="phone-{id}", buttons: form post Aceptar/Rechazar, JS on accept: show phone + disable buttons).
- [] JS en Panel.cshtml y previews: validarPassword() para registro; togglePhone(id) para pendientes (document.getElementById('phone-'+id).style.display='block'; button disabled).

### Dependent Files
- wwwroot/css/registro.css (responsive registro).
- wwwroot/css/index.css (modales, disable-btn, pending-section).
- Views/Inicio/registro.cshtml (form responsive + JS validación).
- wwwroot/preview-registro.html (form actualizado + JS).
- Views/Home/Panel.cshtml (hora modal, clases modales, botón conditional, nuevo section).
- wwwroot/preview-index.html (mismos cambios + ejemplos estáticos pendientes).

### Followup Steps
- [] Actualizar TODO.md al completar cada paso.
- [] Verificar responsive (móviles), JS (validación, toggle), compatibilidad backend (nombres fields/actions como Hora, AceptarDesafio).
- [] Sugerir abrir previews manualmente para ver cambios (browser disabled).

## Pending Tasks
- [ ] Implementar todos los cambios del plan.
- [ ] Backend para nuevos actions (Hora, Aceptar/Rechazar, teléfono visibility) - pendiente compañero.
- [x] Cambiar color del botón de disponibilidad: rojo cuando está disponible (para quitar), verde cuando no está disponible (para habilitar).
- [x] Agregar campo Hora al modal de desafío en Panel.cshtml.
- [x] Agregar gradiente azul al header del modal de edición en index.css.
- [x] Agregar sección "Encuentros Pendientes" en Panel.cshtml con cards de ejemplo, botones Aceptar/Rechazar, y función JS togglePhone.
- [x] Agregar gradiente naranja al header del modal de desafío en index.css.
