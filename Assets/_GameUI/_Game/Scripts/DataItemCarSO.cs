using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataCar", menuName = "ScriptableObjects/DataCar", order = 1)]
public class DataItemCarSO : ScriptableObject
{
    public List<ItemCar> itemCars;
}

[System.Serializable]
public class ItemCar
{
    public int id;                   // ID của xe
    public string name;              // Tên xe
    public Sprite iconItem;          // Hình ảnh Item
    public int modelId;              // ID của mẫu xe
    public int power;                // Công suất (mã lực)
    public DriveType driveType;      // Kiểu dẫn động
    public float price;              // Giá của xe
    public float maxSpeed;           // Tốc độ tối đa (km/h)
    public float acceleration;       // Gia tốc (0-100 km/h trong giây)
    public float nitroCapacity;      // Khả năng Nitro
    public float durability;         // Độ bền của xe
    public Accessory accessory;      // Loại phụ kiện xe
}
public enum Accessory
{
    None = 0,
    FrontBumper = 1000,
    RearBumper = 2000,
    Fender = 3000,
    Spoiler = 4000,
    Rim = 5000,
    Engine = 6000,
    Hood = 7000,
    Skirt = 8000
}
public enum DriveType
{
    FrontWheelDrive = 0,   // Dẫn động cầu trước
    RearWheelDrive = 1000, // Dẫn động cầu sau
    AllWheelDrive = 2000   // Dẫn động 4 bánh
}