using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManageMob : MonoBehaviour
{
//---------------------------------------------
// PUBLIC [S.NS], NOT in unity inspector         
//---------------------------------------------
//---------------------------------------------
// PRIVATE, NOT in unity inspector
//---------------------------------------------
    float timer;
    int count;
    int endcountdown;

    bool ended = false;
    bool showresult = true;
    bool sheepcountstart = true;

    int randomIndex = 0,
        minSizeOfMobPrefabArray = 0,
        maxSizeOfMobPrefabArray = 0,
        minSizeOfFlyingMobPrefabArray = 0,
        maxSizeOfFlyingMobPrefabArray = 0;

    int spawnZoneIndex = 0;

    int spawnZoneChecker = 0;
    int spawnZoneMinChance = 0, spawnZoneMaxChance = 100;
    int spawnNumber = 0;
//---------------------------------------------
// PUBLIC, SHOW in unity inspector
//---------------------------------------------
    public int NumberOfMobsInPlay = 0;    
    public List<GameObject> mobArray = new List<GameObject>();
    public List<int> endMob = new List<int>();
    //---------------------------------------------
    // PRIVATE [SF], SHOW in unity inspector
    //---------------------------------------------
    [SerializeField]
    TMPro.TMP_Text counter;
    
    [SerializeField]
    int baseSpawnZoneCheckerChance = 25;

    [SerializeField]
    float spawntime = 5;

    [SerializeField]
    float revealtime = 1;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject[] mobprefab;

    [SerializeField]
    GameObject[] flyingMobPrefab;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject endspawn;

    [SerializeField]
    GameObject endspawnground;

    [SerializeField]
    GameObject[] spawnzone;

    [SerializeField]
    GameObject[] killzone;

    [SerializeField]
    GameObject countzone;
    
//---------------------------------------------
// FUNCTIONS
//---------------------------------------------      
    void Start()
    {
        timer = spawntime;
        count = 0;

        for (int i = 0; i < mobprefab.Length; ++i)
        {
            ++maxSizeOfMobPrefabArray;
        }

        for (int i = 0; i < flyingMobPrefab.Length; ++i)
        {
            ++maxSizeOfFlyingMobPrefabArray;
        }        
    }

    void Update()
    {
        if (GameManager.Instance.isPause())
        {
            if (Player.GetComponent<Player>().GameTimePassed > 0)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    //choosing a random amount of animal to spawn
                    spawnNumber = Random.Range(1, spawnzone.Length);
                    for (int i = 0; i < spawnNumber; i++)
                    {
                        spawnZoneChecker = Random.Range(spawnZoneMinChance, spawnZoneMaxChance + 1);
                        if (spawnZoneChecker <= baseSpawnZoneCheckerChance)
                        {
                            //Spawn at spawn zone  
                            spawnZoneIndex = Random.Range(1, spawnzone.Length);
                        }
                        else
                            //Spawn at spawn zone  
                            spawnZoneIndex = 0;

                        GameObject mob;
                        if (spawnZoneIndex == 0) // floor spawnZone
                        {
                            // choosing a random animal to spawn
                            randomIndex = Random.Range(minSizeOfMobPrefabArray, maxSizeOfMobPrefabArray);
                            mob = Instantiate(mobprefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
                            mob.GetComponent<TestMob>().ID = randomIndex;
                        }
                        else // flying spawnZone         
                        {
                            // choosing a random flying animal to spawn
                            randomIndex = Random.Range(minSizeOfFlyingMobPrefabArray, maxSizeOfFlyingMobPrefabArray);
                            mob = Instantiate(flyingMobPrefab[randomIndex], spawnzone[spawnZoneIndex].transform.position, Quaternion.identity);
                            mob.GetComponent<TestMob>().ID = mobprefab.Length + randomIndex + 1;
                        }

                        mob.GetComponent<TestMob>().killzone = killzone;
                        mob.GetComponent<TestMob>().manager = this;
                        mob.GetComponent<TestMob>().countzone = countzone;
                        mob.GetComponent<TestMob>().Player = Player;
                        mobArray.Add(mob);
                        NumberOfMobsInPlay++;
                    }

                    timer = Random.Range(spawntime, spawntime + 3);
                }
            }
            else if (NumberOfMobsInPlay == 0)
            {
                if (!ended)
                {
                    mobArray.Clear();
                    ended = true;
                    timer = revealtime;
                    countzone.GetComponent<Collider2D>().isTrigger = false;
                    for (int i = 0; i < endMob.Count; i++)
                    {
                        GameObject mob;
                        if (endMob[i] > mobprefab.Length)
                        {
                            mob = Instantiate(flyingMobPrefab[endMob[i] - mobprefab.Length - 1], endspawn.transform.position, Quaternion.identity);
                            mob.GetComponent<TestMob>().manager = this;
                            mob.GetComponent<TestMob>().end = true;
                        }
                        else
                        {
                            mob = Instantiate(mobprefab[endMob[i]], endspawnground.transform.position, Quaternion.identity);
                            mob.GetComponent<TestMob>().manager = this;
                            mob.GetComponent<TestMob>().end = true;
                        }
                        if (mob.CompareTag("Sheep"))
                        {
                            mobArray.Add(mob);
                        }
                        //endMob.RemoveAt(i);
                    }
                    endcountdown = 1;
                    animator.enabled = true;
                    animator.SetBool("isEnd", true);
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (endcountdown <= count && sheepcountstart)
                    {
                        StartCoroutine(countSheep());
                    }
                    else if(endcountdown >= count && showresult)
                    {
                        StartCoroutine(showResult());
                        showresult = false;
                    }
                    timer = revealtime;
                }
            }
            counter.text = count.ToString();
        }
            
    }
    public void DelKillZone()
    {
        for (int i = 0; i < killzone.Length; ++i)
        {
            Destroy(killzone[i]);
        }
    }
    public void AddCount()
    {
        count++;
    }
    public void DecCount()
    {
        if(count > 0)
        {
            count--;
        }
    }
    IEnumerator countSheep()
    {
        sheepcountstart = false;
        mobArray[endcountdown - 1].gameObject.transform.Find("count").gameObject.GetComponent<TMPro.TMP_Text>().text = endcountdown.ToString();
        Vector3 ogscale = mobArray[endcountdown - 1].gameObject.transform.localScale;

        for (float alpha = 0f; alpha <= 1; alpha += 0.1f)
        {
            mobArray[endcountdown - 1].gameObject.transform.Find("count").gameObject.GetComponent<TMPro.TMP_Text>().alpha = alpha;
            mobArray[endcountdown - 1].gameObject.transform.localScale = Vector3.Lerp(ogscale, ogscale * 1.5f, alpha);
            mobArray[endcountdown - 1].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            mobArray[endcountdown - 1].gameObject.transform.Find("count").gameObject.GetComponent<TMPro.TMP_Text>().alpha = alpha;
            mobArray[endcountdown - 1].gameObject.transform.localScale = Vector3.Lerp(ogscale, ogscale * 1.5f, alpha);
            yield return null;
        }
        mobArray[endcountdown - 1].gameObject.transform.Find("count").gameObject.GetComponent<TMPro.TMP_Text>().alpha = 0;
        endcountdown++;
        sheepcountstart = true;
    }
    IEnumerator showResult()
    {
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            Player.transform.Find("playercounter").gameObject.GetComponent<TMPro.TMP_Text>().alpha = alpha;
            yield return null;
        }
        Player.transform.Find("playercounter").gameObject.GetComponent<TMPro.TMP_Text>().alpha = 0;

        Player.transform.Find("playercounter").gameObject.GetComponent<TMPro.TMP_Text>().text =
                            Player.GetComponent<Player>().GetPlayerCount().ToString() + "/" + count.ToString();
        Player.transform.Find("playercounter").gameObject.GetComponent<TMPro.TMP_Text>().fontSize = 15;

        for (float alpha = 0f; alpha <= 1; alpha += 0.1f)
        {
            Player.transform.Find("playercounter").gameObject.GetComponent<TMPro.TMP_Text>().alpha = alpha;
            yield return null;
        }
        Player.transform.Find("playercounter").gameObject.GetComponent<TMPro.TMP_Text>().alpha = 1;
        if(Player.GetComponent<Player>().GetPlayerCount() == count)
        {
            Player.transform.Find("tick").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("tick").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("tick").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("tick").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("tick").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("tick").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("tick").gameObject.SetActive(true);
        }
        else
        {
            Player.transform.Find("cross").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("cross").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("cross").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("cross").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("cross").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("cross").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f); ;
            Player.transform.Find("cross").gameObject.SetActive(true);
        }
    }
}
