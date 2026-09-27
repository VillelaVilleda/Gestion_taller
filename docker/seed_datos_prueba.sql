-- Datos de prueba para desarrollo local (issue #48).
-- Seguro de correr varias veces: usa ON CONFLICT para no duplicar filas.

BEGIN;

-- ===================== CLIENTES =====================
INSERT INTO cliente ("Id", nombre, telefono, correo, direccion) VALUES
    ('11111111-1111-1111-1111-111111111111', 'Juan Perez', '5555-1234', 'juan.perez@example.com', 'Zona 1, Ciudad'),
    ('22222222-2222-2222-2222-222222222222', 'Maria Lopez', '5555-5678', 'maria.lopez@example.com', 'Zona 5, Ciudad'),
    ('33333333-3333-3333-3333-333333333333', 'Carlos Ramirez', '5555-9012', NULL, 'Zona 10, Ciudad'),
    ('44444444-4444-4444-4444-444444444444', 'Ana Torres', NULL, 'ana.torres@example.com', NULL)
ON CONFLICT ("Id") DO NOTHING;

-- ===================== EMPLEADOS =====================
-- Contrasenas ya hasheadas con BCrypt (compatibles con PasswordHasher, issue #47):
--   admin      -> admin123
--   usuario    -> usuario123
--   cmendez    -> carlos123
--   mgonzalez  -> maria123
INSERT INTO empleado ("Id", nombre, nombre_usuario, password_usuario, es_administrador) VALUES
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Administrador', 'admin', '$2b$11$cZ.ele80ckhfGLDD61TkHuPw/RfDKmKz10ISqRFmwSUt9avXXkQ/K', TRUE),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Empleado de prueba', 'usuario', '$2b$11$Yy2R6NN.OKYw1L6M2oI2duy20VJ/ZqWuCODU5t14RdBtDLn1Op39G', FALSE),
    ('cccccccc-cccc-cccc-cccc-cccccccccccc', 'Carlos Mendez', 'cmendez', '$2b$11$md9d4O3RfhHcZEy6g/QSi.QrGTnFrIwxrwX.TI/FTU63aixEClSeq', FALSE),
    ('dddddddd-dddd-dddd-dddd-dddddddddddd', 'Maria Gonzalez', 'mgonzalez', '$2b$11$QMA27Kz5PLX.Bu9GPojGCupoxXUdzxwjsGhp4nNyXeNbodPocSude', TRUE)
ON CONFLICT ("Id") DO NOTHING;

-- ===================== REPUESTOS =====================
INSERT INTO repuesto ("Id", nombre, existencia) VALUES
    ('e1111111-1111-1111-1111-111111111111', 'Filtro de aceite', 20),
    ('e2222222-2222-2222-2222-222222222222', 'Pastillas de freno', 15),
    ('e3333333-3333-3333-3333-333333333333', 'Bujia', 40),
    ('e4444444-4444-4444-4444-444444444444', 'Correa de distribucion', 5),
    ('e5555555-5555-5555-5555-555555555555', 'Bateria 12v', 8)
ON CONFLICT ("Id") DO NOTHING;

-- ===================== ORDENES =====================
-- Una orden por cada estado (y una extra para el caso de cotizacion rechazada).

-- o1: Recepcion
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('01111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111',
     'Vehiculo sedan color rojo, placa ABC-123', 'El vehiculo no enciende', 15.00, 'Recepcion', NOW() - INTERVAL '1 day',
     '', '', '', 0, FALSE, '', '', '', 0, NULL)
ON CONFLICT ("Id") DO NOTHING;

-- o2: Diagnostico
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('02222222-2222-2222-2222-222222222222', '22222222-2222-2222-2222-222222222222',
     'Motocicleta 150cc', 'Hace un ruido extrano en el motor', 20.00, 'Diagnostico', NOW() - INTERVAL '3 days',
     'Carlos Mendez', 'Se detecto desgaste en la cadena de distribucion', '', 0, FALSE, '', '', '', 0, NULL)
ON CONFLICT ("Id") DO NOTHING;

-- o3: Cotizacion
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('03333333-3333-3333-3333-333333333333', '33333333-3333-3333-3333-333333333333',
     'Pickup color blanco', 'Los frenos chillan al frenar', 15.00, 'Cotizacion', NOW() - INTERVAL '4 days',
     'Maria Gonzalez', 'Pastillas de freno desgastadas', 'Maria Gonzalez', 350.00, FALSE, '', '', '', 0, NULL)
ON CONFLICT ("Id") DO NOTHING;

-- o4: EnReparacion
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('04444444-4444-4444-4444-444444444444', '44444444-4444-4444-4444-444444444444',
     'Sedan compacto azul', 'Necesita cambio de bateria y revision general', 15.00, 'EnReparacion', NOW() - INTERVAL '5 days',
     'Carlos Mendez', 'Bateria descargada, requiere cambio', 'Carlos Mendez', 180.00, TRUE,
     'Maria Gonzalez', '', '', 0, NULL)
ON CONFLICT ("Id") DO NOTHING;

-- o5: Terminado (reparacion completada normalmente)
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('05555555-5555-5555-5555-555555555555', '11111111-1111-1111-1111-111111111111',
     'Camioneta doble cabina', 'Cambio de correa de distribucion', 20.00, 'Terminado', NOW() - INTERVAL '7 days',
     'Maria Gonzalez', 'Correa de distribucion con desgaste severo', 'Maria Gonzalez', 450.00, TRUE,
     'Carlos Mendez', 'Se reemplazo la correa de distribucion y se reviso el tensor', '', 0, NULL)
ON CONFLICT ("Id") DO NOTHING;

-- o6: Terminado (cotizacion rechazada, salta EnReparacion)
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('06666666-6666-6666-6666-666666666666', '22222222-2222-2222-2222-222222222222',
     'Motocicleta 250cc', 'Ruido en la suspension', 15.00, 'Terminado', NOW() - INTERVAL '6 days',
     'Carlos Mendez', 'Amortiguador trasero danado', 'Carlos Mendez', 600.00, FALSE, '', '', '', 0, NULL)
ON CONFLICT ("Id") DO NOTHING;

-- o7: Entregado
INSERT INTO orden ("Id", cliente_id, descripcion_objeto, descripcion_problema, costo_diagnostico, estado, fecha_recepcion,
    empleado_diagnostico, detalle_diagnostico, empleado_cotizacion, monto_cotizado, cotizacion_aceptada,
    empleado_reparacion, detalle_reparacion, empleado_entrega, monto_pagado, fecha_entrega) VALUES
    ('07777777-7777-7777-7777-777777777777', '33333333-3333-3333-3333-333333333333',
     'Sedan color negro', 'Revision de frenos y cambio de pastillas', 15.00, 'Entregado', NOW() - INTERVAL '10 days',
     'Maria Gonzalez', 'Pastillas delanteras y traseras gastadas', 'Maria Gonzalez', 220.00, TRUE,
     'Carlos Mendez', 'Se cambiaron pastillas delanteras y traseras', 'Maria Gonzalez', 220.00, NOW() - INTERVAL '1 hour')
ON CONFLICT ("Id") DO NOTHING;

-- ===================== HISTORIAL DE ESTADOS =====================
-- Debe reflejar exactamente los mismos estados por los que paso cada orden.
INSERT INTO orden_historial_estado ("OrdenId", estado, orden_posicion) VALUES
    ('01111111-1111-1111-1111-111111111111', 'Recepcion', 0),

    ('02222222-2222-2222-2222-222222222222', 'Recepcion', 0),
    ('02222222-2222-2222-2222-222222222222', 'Diagnostico', 1),

    ('03333333-3333-3333-3333-333333333333', 'Recepcion', 0),
    ('03333333-3333-3333-3333-333333333333', 'Diagnostico', 1),
    ('03333333-3333-3333-3333-333333333333', 'Cotizacion', 2),

    ('04444444-4444-4444-4444-444444444444', 'Recepcion', 0),
    ('04444444-4444-4444-4444-444444444444', 'Diagnostico', 1),
    ('04444444-4444-4444-4444-444444444444', 'Cotizacion', 2),
    ('04444444-4444-4444-4444-444444444444', 'EnReparacion', 3),

    ('05555555-5555-5555-5555-555555555555', 'Recepcion', 0),
    ('05555555-5555-5555-5555-555555555555', 'Diagnostico', 1),
    ('05555555-5555-5555-5555-555555555555', 'Cotizacion', 2),
    ('05555555-5555-5555-5555-555555555555', 'EnReparacion', 3),
    ('05555555-5555-5555-5555-555555555555', 'Terminado', 4),

    ('06666666-6666-6666-6666-666666666666', 'Recepcion', 0),
    ('06666666-6666-6666-6666-666666666666', 'Diagnostico', 1),
    ('06666666-6666-6666-6666-666666666666', 'Cotizacion', 2),
    ('06666666-6666-6666-6666-666666666666', 'Terminado', 3),

    ('07777777-7777-7777-7777-777777777777', 'Recepcion', 0),
    ('07777777-7777-7777-7777-777777777777', 'Diagnostico', 1),
    ('07777777-7777-7777-7777-777777777777', 'Cotizacion', 2),
    ('07777777-7777-7777-7777-777777777777', 'EnReparacion', 3),
    ('07777777-7777-7777-7777-777777777777', 'Terminado', 4),
    ('07777777-7777-7777-7777-777777777777', 'Entregado', 5);

COMMIT;