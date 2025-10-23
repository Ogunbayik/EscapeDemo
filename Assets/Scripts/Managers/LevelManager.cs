using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levelList = new List<GameObject>();
    public List<GameObject> moveableList = new List<GameObject>();
    public static LevelManager Instance;

    [Header("Level Settings")]
    public int currentLevelIndex;
    public int activeLevelCount;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        #endregion
    }
    void Start()
    {
        ActivateLevels(currentLevelIndex, activeLevelCount);
        SetMoveableList();
    }

    private void ActivateLevels(int levelIndex, int count)
    {
        currentLevelIndex = levelIndex;
        count = currentLevelIndex + activeLevelCount;

        for (int i = 0; i < levelList.Count; i++)
        {
            levelList[i].SetActive(false);

            for (int j = currentLevelIndex; j < count; j++)
            {
                levelList[j].SetActive(true);
            }
        }
    }

    private void SetMoveableList()
    {
        moveableList.Clear();

        foreach (Transform moveableObj in levelList[currentLevelIndex].transform)
        {
            for (int i = 0; i < moveableObj.childCount; i++)
            {
                if (moveableObj.GetChild(i).gameObject.CompareTag("Moveable"))
                    moveableList.Add(moveableObj.GetChild(i).gameObject);
            }
        }
    }
    public List<GameObject> GetMoveableList()
    {
        return moveableList;
    }
    public void PassLevel()
    {
        currentLevelIndex++;
        ActivateLevels(currentLevelIndex, activeLevelCount);
        SetMoveableList();
    }


}
