namespace TransportCompany.Domain.Shared;

/// <summary>
/// Тип кузова транспортного средства
/// </summary>
public enum BodyType
{
    /// <summary>
    /// Тентованный кузов
    /// </summary>
    Tent,

    /// <summary>
    /// Рефрижераторный кузов
    /// </summary>
    Refrigerator,

    /// <summary>
    /// Цистерна
    /// </summary>
    Tank,

    /// <summary>
    /// Бортовая платформа
    /// </summary>
    Flatbed,

    /// <summary>
    /// Контейнерная площадка
    /// </summary>
    Container,

    /// <summary>
    /// Изотермический кузов
    /// </summary>
    Isothermal
}