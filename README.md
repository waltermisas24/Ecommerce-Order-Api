# Ecommerce.Order.API

Este proyecto es una API RESTful construida con ASP.NET Core que forma parte de un sistema de ecommerce modular. El microservicio `Ecommerce.Order.API` está encargado de gestionar órdenes de compra y sus respectivos ítems.

## Características

- Creación de órdenes (`OrderInfo`) con múltiples ítems (`OrderItem`)
- Relación uno a muchos entre `OrderInfo` y `OrderItem`
- Operaciones CRUD para órdenes e ítems
- Eliminación en cascada de ítems al eliminar o actualizar una orden
- Manejo de errores centralizado
- Migraciones con Entity Framework Core
- Arquitectura limpia: separación de capas (Domain, Infrastructure, Application, API)

## Estructura del Proyecto

```
Ecommerce.Order.API/
│
├── Ecommerce.Order.Domain/         → Entidades y contratos de dominio
├── Ecommerce.Order.Infrastructure/ → Acceso a datos (EF Core), contexto y repositorios
├── Ecommerce.Order.Application/    → Servicios de aplicación (lógica de negocio)
├── Ecommerce.Order.API/            → Controladores y configuración principal
└── README.md
```

## Entidades Principales

### OrderInfo

```csharp
public class OrderInfo
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; set; }
    public string Status { get; set; } = "Created";
    public List<OrderItem> Items { get; set; } = new();
}
```

### OrderItem

```csharp
public class OrderItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string OrderInfoId { get; set; }
    [JsonIgnore]
    public OrderInfo? OrderInfo { get; set; }
}
```

## Endpoints Principales

### POST `/api/v1/orders`

Crea una nueva orden con ítems.

### GET `/api/v1/orders/{id}`

Obtiene los detalles de una orden específica por su ID.

### PUT `/api/v1/orders/{id}/items`

Actualiza los ítems de una orden específica. Este endpoint reemplaza todos los ítems actuales por los nuevos enviados en el cuerpo del request.

## Setup y Migraciones

### 1. Crear la base de datos y aplicar migraciones

```
dotnet ef migrations add InitialCreate --project Ecommerce.Order.Infrastructure --startup-project Ecommerce.Order.API
dotnet ef database update --project Ecommerce.Order.Infrastructure --startup-project Ecommerce.Order.API

```

La API estará disponible en: `https://localhost:{puerto}/swagger`

## Pruebas en Swagger

Puedes probar todos los endpoints directamente desde la interfaz Swagger disponible al correr la API.

Ejemplo de JSON para creación

```json
{
  "customerId": "cliente001",
  "items": [
    { "productId": "PROD-001", "quantity": 2, "price": 25.99 },
    { "productId": "PROD-002", "quantity": 1, "price": 9.99 }
  ]
}

Para actualizar ítems de una orden, puedes usar el siguiente formato:

```json
[
  {
    "id": "ITEM-ID-1",
    "productId": "PROD-001",
    "quantity": 2,
    "price": 12.99
  },
  {
    "id": "ITEM-ID-2",
    "productId": "PROD-002",
    "quantity": 1,
    "price": 24.99
  }
]
```

## Notas de diseño

-Se utiliza código-first con EF Core y SQL Server.
-La propiedad OrderInfoId se maneja internamente para mantener la integridad referencial.
-Se eliminan los ítems viejos antes de insertar los nuevos, evitando conflictos de clave foránea o tracking.
-La limpieza de migraciones se puede rehacer repitiendo los pasos anteriores.

## Tecnologías Usadas

- Net 8.0
- Entity Framework Core
- SQL Server
- Swagger UI
