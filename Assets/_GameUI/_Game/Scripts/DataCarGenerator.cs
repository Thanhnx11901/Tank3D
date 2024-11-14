using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataCarGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate DataItemCarSO")]
    public static void GenerateDataCar()
    {
        // Tạo một instance mới của DataItemCarSO
        DataItemCarSO dataCarSO = ScriptableObject.CreateInstance<DataItemCarSO>();
        dataCarSO.itemCars = new List<ItemCar>();

        // Tạo dữ liệu mẫu cho ItemCar
        for (int i = 1; i <= 10; i++) // Giả sử tạo 10 xe
        {
            ItemCar car = new ItemCar
            {
                id = i,
                name = "Car " + i,
                //iconItem = Resources.Load<Sprite>($"CarIcons/CarIcon_{i}"), // Đảm bảo các sprite này có trong thư mục Resources/CarIcons
                modelId = i,
                power = Random.Range(100, 400),
                driveType = (DriveType)Random.Range(0, 3),
                price = Random.Range(20000, 100000),
                maxSpeed = Random.Range(150, 300),
                acceleration = Random.Range(3.0f, 10.0f),
                nitroCapacity = Random.Range(1.0f, 5.0f),
                durability = Random.Range(50, 100),
                accessory = (Accessory)Random.Range(0, 9)
            };

            // Thêm car vào danh sách
            dataCarSO.itemCars.Add(car);
        }

        // Lưu file SO
        AssetDatabase.CreateAsset(dataCarSO, "Assets/DataCarSO.asset");
        AssetDatabase.SaveAssets();

        Debug.Log("DataCarSO đã được tạo và lưu thành công tại Assets/Resources/DataCarSO.asset");
    }
}
