
# DCA – Crypto Dollar Cost Averaging Tracker

Aplicación web desarrollada en **ASP.NET Core MVC** para llevar el control de compras de criptomonedas bajo la estrategia **Dollar Cost Averaging (DCA)**, con cálculo de costos en **USDT** y **pesos mexicanos (MXN)** utilizando el método **FIFO (First In, First Out)**.

Este proyecto está pensado para uso personal y educativo, con una arquitectura simple y explícita que facilita la extensión futura.

---

## 🎯 Objetivo del sistema

Permitir registrar:

- Fondos disponibles (principalmente USDT) con su costo en MXN
- Compras de criptomonedas usando DCA
- Cálculo automático del costo en MXN por **FIFO**
- Visualizar el portafolio con:
  - Costos promedio
  - Valor actual
  - Ganancia o pérdida en MXN

Todo el sistema funciona **sin base de datos real** en esta primera versión; la persistencia se simula directamente en los controllers.

---

## 🧱 Arquitectura

- **Backend:** ASP.NET Core MVC
- **Frontend:** Razor + JavaScript vanilla
- **Patrón:** MVC clásico
- **DTOs:** Para toda la operatividad (incluye DTOs anidados)
- **Persistencia:** Simulada en Controllers (sin ORM, sin repositorios)
- **Autenticación:** Cookie Authentication con credenciales hardcodeadas en `appsettings.json`

---

## 🔐 Autenticación

- Login obligatorio
- Usuario y contraseña definidos en `appsettings.json`
- No existe catálogo de usuarios
- No hay registro ni roles
- Todo el sistema está protegido excepto la pantalla de login

---

## 💰 Manejo de Fondos (FIFO)

Los fondos se manejan como **lotes** (por ejemplo, compras de USDT):

Cada lote contiene:
- Fecha
- Moneda
- Cantidad inicial
- Cantidad disponible
- Costo unitario en MXN

El sistema aplica **FIFO** para calcular el costo en MXN cuando se usan los fondos para comprar criptomonedas.

---

## 🔄 Operaciones DCA

En cada operación de compra se registra:

- Fecha
- Moneda comprada (BTC, ETH, etc.)
- Moneda de pago (USDT)
- Cantidad comprada
- Costo en USDT
- **Costo en MXN (calculado automáticamente por FIFO)**
- Detalle del consumo FIFO por lote

El usuario **no captura** el costo en MXN.

---

## 📊 Dashboard

La vista principal muestra:

- Tabla por activo:
  - Cantidad actual
  - Costo promedio en USDT
  - Costo promedio en MXN
  - Total invertido en USDT
  - Total invertido en MXN
  - Precio actual
  - Valor actual en MXN
  - Ganancia / pérdida en MXN y porcentaje

- Totales generales del portafolio

La valuación se calcula como si el portafolio se vendiera al precio actual.

---


---

## ⚠️ Notas importantes

- No se conecta a ninguna API externa real
- No se utiliza base de datos en esta versión
- La lógica financiera está separada de la persistencia
- El sistema está diseñado para evolucionar fácilmente:
  - Base de datos
  - Ventas
  - Más monedas
  - Gráficas
  - APIs de precios reales

---

## 🚀 Estado del proyecto

Versión inicial en desarrollo.  
Este repositorio se utiliza como base para evolución incremental del sistema.

---

## 📄 Licencia

Uso personal / educativo.  
Sin fines comerciales en su estado actual.

