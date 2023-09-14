using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyNewTest : MonoBehaviour
{
    public Transform waterSurface; // ส่วนสูงของพื้นผิวน้ำ
    public float buoyancyForce = 10f; // แรงบั๊กแรงซับซ้อน
    public LayerMask buoyantObjectsLayer; // ชั้นของออบเจ็กต์ที่ต้องการให้ลอย

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // ตรวจสอบว่าออบเจ็กต์อยู่ในพื้นที่น้ำหรือไม่
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity, buoyantObjectsLayer))
        {
            // คำนวณแรงบั๊กแรงซับซ้อนตามส่วนสูงระหว่างออบเจ็กต์และพื้นผิวน้ำ
            float distanceToSurface = waterSurface.position.y - hitInfo.point.y;
            float buoyancy = Mathf.Clamp01(distanceToSurface / 2f); // การจำกัดค่าระหว่าง 0 ถึง 1

            // สร้างแรงบั๊กแรงซับซ้อนที่เป็นทิศลง
            Vector3 buoyantForce = Vector3.up * buoyancyForce * buoyancy;

            // ใช้แรงบั๊กแรงซับซ้อนลงใน Rigidbody
            rb.AddForceAtPosition(buoyantForce, hitInfo.point, ForceMode.Force);
        }
    }
}
