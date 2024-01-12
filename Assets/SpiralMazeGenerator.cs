using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMazeGenerator : MonoBehaviour
{
    public GameObject[] allSprites;
    public Sprite[] sprites;
    [Space(30)]
    public GameObject wallPrefab;    // Префаб стены
    public float radius = 10f;        // Радиус игровой зоны
    public int numWalls = 100;        // Количество стен
    public float a = 0.1f;            // Параметр для определения плотности спирали
    public float rotationSpeed = 1f;  // Скорость вращения стен

    void Start()
    {
        for (int i = 0; i < allSprites.Length; i++) 
        {
            allSprites[i].transform.localScale = new Vector2(Random.Range(4.6f, 5.05f), Random.Range(4.6f, 5.05f));
            int randomInt = Random.Range(0, 4);
            allSprites[i].GetComponent<SpriteRenderer>().sprite = sprites[randomInt];
            if(randomInt < 2)
            {
                allSprites[i].GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                allSprites[i].GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        //GenerateSpiralMaze();
    }

    void GenerateSpiralMaze()
    {
        float centerX = transform.position.x;
        float centerY = transform.position.y;

        for (int i = 0; i < numWalls; i++)
        {
            // Параметрическое уравнение спирали
            float theta = a * i;
            float x = centerX + radius * theta * Mathf.Cos(theta);
            float y = centerY + radius * theta * Mathf.Sin(theta);

            Vector2 spawnPosition = new Vector2(x, y);

            // Помещаем стену на сцену
            GameObject wall = Instantiate(wallPrefab, spawnPosition, Quaternion.identity, transform);

            // Рассчитываем угол вращения для стены
            float rotationAngle = Mathf.Atan2(centerY - y, centerX - x) * Mathf.Rad2Deg + 90f;

            // Применяем вращение к стене
            wall.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

            // Вращаем стену постепенно
            RotateWallOverTime(wall, rotationSpeed);
        }
    }

    void RotateWallOverTime(GameObject wall, float speed)
    {
        StartCoroutine(RotateWallCoroutine(wall, speed));
    }

    IEnumerator RotateWallCoroutine(GameObject wall, float speed)
    {
        while (true)
        {
            wall.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }
    }
}
