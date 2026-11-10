-- Datos de prueba: taller de electrónicos (laptops, PCs, teléfonos, discos duros...).
-- BORRA todos los datos de la base (no toca la tabla de migraciones) y carga datos nuevos:
-- 11 clientes, 6 empleados (1 administrador), 16 repuestos y 13 órdenes:
-- 1 en Recepción, 1 en Diagnóstico, 3 en Cotización, 1 en Reparación, 2 en Terminado y 5 en Entregado.
-- monto_cotizado = costo de mano de obra; total = diagnóstico + mano de obra + repuestos (solo si se aceptó).
-- La existencia de los repuestos ya refleja los descuentos de las cotizaciones aceptadas.
-- Las fechas se escriben en hora de Guatemala (-06); la aplicación las muestra en hora local.

SET client_encoding = 'UTF8';
BEGIN;

TRUNCATE TABLE orden_repuesto, orden_historial_estado, orden, repuesto, empleado, cliente RESTART IDENTITY CASCADE;

-- ===================== CLIENTES =====================
INSERT INTO cliente ("Id", nombre, telefono, correo, direccion) VALUES
    ('c0000000-0000-0000-0000-000000000001', 'Luis Antonio Catalán Pérez', '4012-8856', 'luis.catalan@example.com', 'Zona 1, Ciudad de Guatemala'),
    ('c0000000-0000-0000-0000-000000000002', 'Alan Horacio Cerón Valdez', '5763-2210', NULL, 'Mixco, Guatemala'),
    ('c0000000-0000-0000-0000-000000000003', 'Angel Josué Jiménez Carranza', '3345-9087', 'angel.jimenez@example.com', NULL),
    ('c0000000-0000-0000-0000-000000000004', 'Geovany Jaime Calderón García', NULL, 'geovany.calderon@example.com', 'Villa Nueva, Guatemala'),
    ('c0000000-0000-0000-0000-000000000005', 'Jaime Hengel Cardona Menéndez', '5598-1164', 'jaime.cardona@example.com', 'Zona 18, Ciudad de Guatemala'),
    ('c0000000-0000-0000-0000-000000000006', 'Hengel Obed Contreras Nova', '4471-3029', NULL, 'San Miguel Petapa, Guatemala'),
    ('c0000000-0000-0000-0000-000000000007', 'Obed Óscar García Sandoval', '5236-7745', 'obed.garcia@example.com', 'Zona 7, Ciudad de Guatemala'),
    ('c0000000-0000-0000-0000-000000000008', 'Óscar Cristofer Menéndez Villafuerte', '3189-6502', 'oscar.menendez@example.com', NULL),
    ('c0000000-0000-0000-0000-000000000009', 'Cristofer Misael Nova Zabaleta', '5604-9931', 'cristofer.nova@example.com', 'Santa Catarina Pinula, Guatemala'),
    ('c0000000-0000-0000-0000-000000000010', 'Misael Estuardo Sandoval Alonzo', '4820-1457', NULL, 'Zona 5, Ciudad de Guatemala'),
    ('c0000000-0000-0000-0000-000000000011', 'Estuardo Yahir Villafuerte Calderón', NULL, 'estuardo.villafuerte@example.com', 'Chinautla, Guatemala');

-- ===================== EMPLEADOS =====================
-- Contraseñas guardadas con hash BCrypt. Usuario -> contraseña (solo para pruebas):
--   jose.villeda           -> jose2026   (administrador)
--   saul.marroquin         -> saul2026
--   elias.galdamez         -> elias2026
--   cristopher.espana      -> cristopher2026
--   mario.contreras        -> mario2026
--   yahir.cardona          -> yahir2026
INSERT INTO empleado ("Id", nombre, nombre_usuario, password_usuario, es_administrador) VALUES
    ('e0000000-0000-0000-0000-000000000001', 'José Alfonso Villela Villeda', 'jose.villeda', '$2b$11$W/F2JqHGRj7SoG9xOjRTzOCYaKo6B8AF/TNikbvV5kseQQxMcEnMe', TRUE),
    ('e0000000-0000-0000-0000-000000000002', 'Saúl Andrés Monroy Marroquín', 'saul.marroquin', '$2b$11$mknCXHbc/zfNbgDEVbNwZu0YdtlBEe9tyC5uKs4z2cfw9R5Q4O1bm', FALSE),
    ('e0000000-0000-0000-0000-000000000003', 'Elías Josué Molina Galdamez', 'elias.galdamez', '$2b$11$0hpJNblR7wcCfMK5WoDRQ.u0K.6wyYV841tGjIuHtc/qxjTIpflue', FALSE),
    ('e0000000-0000-0000-0000-000000000004', 'Cristopher Alberto León España', 'cristopher.espana', '$2b$11$CDpgGeZVEb9bhGZVNKr2DOgnCFdFqBhDtSS9EwTVNX15hV8FwqLFq', FALSE),
    ('e0000000-0000-0000-0000-000000000005', 'Mario Geovany Alonzo Contreras', 'mario.contreras', '$2b$11$27xAIeQqcxSLqBb78sFZS.XKena4pjL1LT21gLbsZ.oe.9SFg4dnO', FALSE),
    ('e0000000-0000-0000-0000-000000000006', 'Yahir Mario Zabaleta Cardona', 'yahir.cardona', '$2b$11$9fp2bomVWPwFqSJghLdFDeBrtICOuQ19khVNqi7MvfNyVeoNlYXES', FALSE);

-- ===================== REPUESTOS =====================
-- Precio en quetzales. Existencia actual (ya descontadas las cotizaciones aceptadas).
INSERT INTO repuesto ("Id", nombre, existencia, precio) VALUES
    ('a0000000-0000-0000-0000-000000000001', 'Batería para laptop (6 celdas)', 5, 450.00),
    ('a0000000-0000-0000-0000-000000000002', 'Cargador universal para laptop 65 W', 8, 225.00),
    ('a0000000-0000-0000-0000-000000000003', 'Teclado para laptop', 4, 275.00),
    ('a0000000-0000-0000-0000-000000000004', 'Pantalla LCD 15.6" para laptop', 2, 650.00),
    ('a0000000-0000-0000-0000-000000000005', 'Ventilador para laptop', 4, 150.00),
    ('a0000000-0000-0000-0000-000000000006', 'Disco SSD 480 GB', 9, 380.00),
    ('a0000000-0000-0000-0000-000000000007', 'Disco SSD 1 TB', 4, 650.00),
    ('a0000000-0000-0000-0000-000000000008', 'Disco duro 1 TB (HDD)', 5, 360.00),
    ('a0000000-0000-0000-0000-000000000009', 'Memoria RAM DDR4 8 GB', 11, 260.00),
    ('a0000000-0000-0000-0000-000000000010', 'Memoria RAM DDR4 16 GB', 4, 480.00),
    ('a0000000-0000-0000-0000-000000000011', 'Fuente de poder 500 W', 2, 420.00),
    ('a0000000-0000-0000-0000-000000000012', 'Ventilador de CPU 120 mm', 6, 135.00),
    ('a0000000-0000-0000-0000-000000000013', 'Pasta térmica', 19, 45.00),
    ('a0000000-0000-0000-0000-000000000014', 'Pantalla para teléfono 6.5"', 0, 550.00),
    ('a0000000-0000-0000-0000-000000000015', 'Batería para teléfono', 9, 180.00),
    ('a0000000-0000-0000-0000-000000000016', 'Puerto de carga USB-C', 15, 85.00);

-- ===================== ÓRDENES =====================
-- El código que muestra la aplicación es la posición por fecha de recepción (O-01 es la más antigua).
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico_id, detalle_diagnostico, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion_id, detalle_reparacion, empleado_entrega_id, monto_pagado, fecha_entrega, fecha_ultima_edicion) VALUES
    -- O-01 Entregado (cotización aceptada), total Q 675.00
    ('b0000000-0000-0000-0000-000000000001', 'c0000000-0000-0000-0000-000000000003',
     'Laptop Dell Inspiron 15, color negro',
     'La batería no carga y la laptop se apaga al desconectar el cargador.',
     75.00, 'Entregado', '2026-10-05 09:20:00-06',
     'e0000000-0000-0000-0000-000000000005', 'Batería agotada (ciclos al límite); el cargador funciona bien. Se recomienda cambiar la batería.',
     150.00, TRUE,
     'e0000000-0000-0000-0000-000000000003', 'Se cambió la batería y se calibró la carga.',
     'e0000000-0000-0000-0000-000000000002', 675.00, '2026-10-08 16:10:00-06', NULL),
    -- O-02 Entregado (cotización aceptada), total Q 685.00
    ('b0000000-0000-0000-0000-000000000002', 'c0000000-0000-0000-0000-000000000001',
     'PC de escritorio, gabinete negro',
     'No enciende; a veces huele a quemado.',
     100.00, 'Entregado', '2026-10-07 10:45:00-06',
     'e0000000-0000-0000-0000-000000000003', 'Fuente de poder dañada; la tarjeta madre y los demás componentes funcionan.',
     120.00, TRUE,
     'e0000000-0000-0000-0000-000000000006', 'Se cambió la fuente de poder y se hizo limpieza con cambio de pasta térmica.',
     'e0000000-0000-0000-0000-000000000004', 685.00, '2026-10-10 11:30:00-06', NULL),
    -- O-03 Entregado (cotización aceptada), total Q 800.00
    ('b0000000-0000-0000-0000-000000000003', 'c0000000-0000-0000-0000-000000000002',
     'Teléfono Samsung Galaxy A54',
     'Pantalla quebrada; el táctil no responde en la parte de abajo.',
     50.00, 'Entregado', '2026-10-12 14:05:00-06',
     'e0000000-0000-0000-0000-000000000005', 'Pantalla y digitalizador dañados por golpe; el resto del teléfono funciona.',
     200.00, TRUE,
     'e0000000-0000-0000-0000-000000000003', 'Se reemplazó la pantalla completa y se probó el táctil.',
     'e0000000-0000-0000-0000-000000000002', 800.00, '2026-10-14 17:40:00-06', NULL),
    -- O-04 Entregado (cotización rechazada: solo se cobra el diagnóstico), total Q 100.00
    ('b0000000-0000-0000-0000-000000000004', 'c0000000-0000-0000-0000-000000000003',
     'Disco duro externo Seagate 2 TB',
     'La computadora no lo reconoce; necesita recuperar fotos.',
     100.00, 'Entregado', '2026-10-15 08:50:00-06',
     'e0000000-0000-0000-0000-000000000006', 'El disco hace clic: daño en los cabezales. La recuperación requiere laboratorio especializado.',
     0.00, FALSE,
     NULL, '',
     'e0000000-0000-0000-0000-000000000004', 100.00, '2026-10-16 12:15:00-06', NULL),
    -- O-05 Entregado (cotización aceptada), total Q 890.00
    ('b0000000-0000-0000-0000-000000000005', 'c0000000-0000-0000-0000-000000000004',
     'Laptop HP Pavilion 14',
     'Muy lenta; tarda mucho en encender.',
     75.00, 'Entregado', '2026-10-20 11:25:00-06',
     'e0000000-0000-0000-0000-000000000003', 'Disco duro mecánico con sectores dañados y solo 4 GB de RAM.',
     175.00, TRUE,
     'e0000000-0000-0000-0000-000000000006', 'Se instaló el SSD, se clonó el sistema y se agregó memoria RAM.',
     'e0000000-0000-0000-0000-000000000002', 890.00, '2026-10-23 15:00:00-06', NULL),
    -- O-06 Terminado (cotización aceptada), total Q 450.00
    ('b0000000-0000-0000-0000-000000000006', 'c0000000-0000-0000-0000-000000000005',
     'Laptop Lenovo IdeaPad 3',
     'Algunas teclas no funcionan después de derramar café.',
     75.00, 'Terminado', '2026-10-26 09:35:00-06',
     'e0000000-0000-0000-0000-000000000005', 'Teclado dañado por líquido; la tarjeta madre no presenta daños.',
     100.00, TRUE,
     'e0000000-0000-0000-0000-000000000003', 'Se reemplazó el teclado y se limpió la zona del derrame.',
     NULL, 0.00, NULL, NULL),
    -- O-07 Terminado (cotización rechazada: solo se cobra el diagnóstico), total Q 60.00
    ('b0000000-0000-0000-0000-000000000007', 'c0000000-0000-0000-0000-000000000006',
     'Consola PlayStation 4',
     'Se apaga sola después de unos minutos de juego.',
     60.00, 'Terminado', '2026-10-28 16:20:00-06',
     'e0000000-0000-0000-0000-000000000006', 'Sobrecalentamiento por polvo acumulado y pasta térmica seca. El cliente decidió no repararla.',
     0.00, FALSE,
     NULL, '',
     NULL, 0.00, NULL, NULL),
    -- O-08 EnReparacion, total Q 875.00
    ('b0000000-0000-0000-0000-000000000008', 'c0000000-0000-0000-0000-000000000007',
     'Laptop Acer Aspire 5',
     'La pantalla se ve con rayas y parpadea.',
     75.00, 'EnReparacion', '2026-11-02 10:10:00-06',
     'e0000000-0000-0000-0000-000000000005', 'Panel LCD dañado; el cable flex y la tarjeta de video funcionan.',
     150.00, TRUE,
     NULL, '',
     NULL, 0.00, NULL, NULL),
    -- O-09 Cotizacion, total Q 50.00
    ('b0000000-0000-0000-0000-000000000009', 'c0000000-0000-0000-0000-000000000008',
     'iPhone 12',
     'La batería dura muy poco y el teléfono se calienta.',
     50.00, 'Cotizacion', '2026-11-03 13:45:00-06',
     'e0000000-0000-0000-0000-000000000003', 'Batería degradada al 68 % de su capacidad.',
     0.00, FALSE,
     NULL, '',
     NULL, 0.00, NULL, NULL),
    -- O-10 Cotizacion, total Q 100.00
    ('b0000000-0000-0000-0000-000000000010', 'c0000000-0000-0000-0000-000000000009',
     'PC gamer, gabinete con luces RGB',
     'Se reinicia cuando juega.',
     100.00, 'Cotizacion', '2026-11-04 09:00:00-06',
     'e0000000-0000-0000-0000-000000000006', 'Procesador con temperatura alta: pasta térmica seca y ventilador del disipador con falla.',
     0.00, FALSE,
     NULL, '',
     NULL, 0.00, NULL, NULL),
    -- O-11 Cotizacion, total Q 50.00
    ('b0000000-0000-0000-0000-000000000011', 'c0000000-0000-0000-0000-000000000001',
     'Tablet Samsung Galaxy Tab A8',
     'No carga; el cable se mueve dentro del puerto.',
     50.00, 'Cotizacion', '2026-11-05 15:30:00-06',
     'e0000000-0000-0000-0000-000000000005', 'Puerto de carga USB-C flojo y con la soldadura fracturada.',
     0.00, FALSE,
     NULL, '',
     NULL, 0.00, NULL, NULL),
    -- O-12 Diagnostico, total Q 75.00
    ('b0000000-0000-0000-0000-000000000012', 'c0000000-0000-0000-0000-000000000010',
     'Laptop ASUS VivoBook 15',
     'No da imagen; solo se enciende la luz de encendido.',
     75.00, 'Diagnostico', '2026-11-06 11:15:00-06',
     NULL, '',
     0.00, FALSE,
     NULL, '',
     NULL, 0.00, NULL, NULL),
    -- O-13 Recepcion, total Q 50.00
    ('b0000000-0000-0000-0000-000000000013', 'c0000000-0000-0000-0000-000000000011',
     'Monitor LG de 24 pulgadas',
     'Tiene una línea vertical en la pantalla.',
     50.00, 'Recepcion', '2026-11-07 17:05:00-06',
     NULL, '',
     0.00, FALSE,
     NULL, '',
     NULL, 0.00, NULL, NULL);

-- ===================== REPUESTOS POR ORDEN =====================
-- precio_unitario = precio del repuesto al cotizar. En Cotización todavía no se descuentan del inventario.
INSERT INTO orden_repuesto (orden_id, repuesto_id, cantidad, precio_unitario) VALUES
    ('b0000000-0000-0000-0000-000000000001', 'a0000000-0000-0000-0000-000000000001', 1, 450.00),
    ('b0000000-0000-0000-0000-000000000002', 'a0000000-0000-0000-0000-000000000011', 1, 420.00),
    ('b0000000-0000-0000-0000-000000000002', 'a0000000-0000-0000-0000-000000000013', 1, 45.00),
    ('b0000000-0000-0000-0000-000000000003', 'a0000000-0000-0000-0000-000000000014', 1, 550.00),
    ('b0000000-0000-0000-0000-000000000005', 'a0000000-0000-0000-0000-000000000006', 1, 380.00),
    ('b0000000-0000-0000-0000-000000000005', 'a0000000-0000-0000-0000-000000000009', 1, 260.00),
    ('b0000000-0000-0000-0000-000000000006', 'a0000000-0000-0000-0000-000000000003', 1, 275.00),
    ('b0000000-0000-0000-0000-000000000008', 'a0000000-0000-0000-0000-000000000004', 1, 650.00),
    ('b0000000-0000-0000-0000-000000000009', 'a0000000-0000-0000-0000-000000000015', 1, 180.00),
    ('b0000000-0000-0000-0000-000000000010', 'a0000000-0000-0000-0000-000000000012', 1, 135.00),
    ('b0000000-0000-0000-0000-000000000010', 'a0000000-0000-0000-0000-000000000013', 1, 45.00);

-- ===================== HISTORIAL DE ESTADOS =====================
-- Las etapas por las que pasó cada orden, en orden (una cotización rechazada salta EnReparacion).
INSERT INTO orden_historial_estado ("OrdenId", estado, orden_posicion) VALUES
    ('b0000000-0000-0000-0000-000000000001', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000001', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000001', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000001', 'EnReparacion', 3),
    ('b0000000-0000-0000-0000-000000000001', 'Terminado', 4),
    ('b0000000-0000-0000-0000-000000000001', 'Entregado', 5),
    ('b0000000-0000-0000-0000-000000000002', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000002', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000002', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000002', 'EnReparacion', 3),
    ('b0000000-0000-0000-0000-000000000002', 'Terminado', 4),
    ('b0000000-0000-0000-0000-000000000002', 'Entregado', 5),
    ('b0000000-0000-0000-0000-000000000003', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000003', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000003', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000003', 'EnReparacion', 3),
    ('b0000000-0000-0000-0000-000000000003', 'Terminado', 4),
    ('b0000000-0000-0000-0000-000000000003', 'Entregado', 5),
    ('b0000000-0000-0000-0000-000000000004', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000004', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000004', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000004', 'Terminado', 3),
    ('b0000000-0000-0000-0000-000000000004', 'Entregado', 4),
    ('b0000000-0000-0000-0000-000000000005', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000005', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000005', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000005', 'EnReparacion', 3),
    ('b0000000-0000-0000-0000-000000000005', 'Terminado', 4),
    ('b0000000-0000-0000-0000-000000000005', 'Entregado', 5),
    ('b0000000-0000-0000-0000-000000000006', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000006', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000006', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000006', 'EnReparacion', 3),
    ('b0000000-0000-0000-0000-000000000006', 'Terminado', 4),
    ('b0000000-0000-0000-0000-000000000007', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000007', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000007', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000007', 'Terminado', 3),
    ('b0000000-0000-0000-0000-000000000008', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000008', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000008', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000008', 'EnReparacion', 3),
    ('b0000000-0000-0000-0000-000000000009', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000009', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000009', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000010', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000010', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000010', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000011', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000011', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000011', 'Cotizacion', 2),
    ('b0000000-0000-0000-0000-000000000012', 'Recepcion', 0),
    ('b0000000-0000-0000-0000-000000000012', 'Diagnostico', 1),
    ('b0000000-0000-0000-0000-000000000013', 'Recepcion', 0);

COMMIT;
