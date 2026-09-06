CREATE TABLE analisis_solicitudes_internacion_send_dcm4che (
  id VARCHAR(50) NOT NULL PRIMARY KEY,
  opt1 VARCHAR(255) NULL,
  opt2 VARCHAR(255) NULL,
  opt3 VARCHAR(255) NULL,
  estado VARCHAR(255) NULL,
  fecha_hora DATETIME NULL,
  fk_nrosolicitud VARCHAR(50) NULL
);

ALTER TABLE analisis_solicitudes_internacion_send_dcm4che ADD estado VARCHAR(255) NULL;
