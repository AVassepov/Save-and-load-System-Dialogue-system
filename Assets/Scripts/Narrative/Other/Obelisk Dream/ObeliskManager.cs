using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObeliskManager : MonoBehaviour
{
    [SerializeField] private GameObject ObeliskPrefab;
    public List<Sign> ObeliskInstances;
    public string PlayerName;
    private Player player;

    public string[] MaleFirstNames = new string[]
    {
        "Anton", "Boris", "Bratan", "Cvetko", "Darko", "Dušan", "Duško", "Gniewko", "Goran", "Gvozden", "Lech",
        "Leszek", "Lubo", "Miloš", "Mladen", "Ognjen", "Plamen", "Slava", "Tvrtko", "Veselin", "Vlad", "Vuk", "Yasen",
        "Zdravko", "Živan", "Blahoslav", "Ivan", "Milan", "Mylo", "Damir", "Kole", "Vladimir", "Vlasta", "Ladislav",
        "Miran", "Ladislaus", "Miroslaw", "Wenceslaus", "Bronislaw", "Vaclav", "Novak", "Pavel", "Tomislav", "Radoslav",
        "Boban", "Valeri", "Vasily", "Gelina", "Jovann", "Danek", "Illya", "Kass", "Casimiro", "Wojciech", "Borja",
        "Andric", "Juri", "Andrick", "Radovid", "Stannis"
    };

    public string[] FemaleFirstNames = new string[]
    {
        "Masha", "Anna", "Vera", "Aniyah", "Lana", "Ivanna", "Milena", "Zaria", "Zora", "Raina", "Ivana", "Vayda",
        "Sonia", "Karolina", "Kalina", "Danica", "Yana", "Mileena", "Iva", "Danika", "Zofia", "Jarmila", "Miran",
        "Bronislawa", "Ivanka", "Wladyslawa", "Slavko", "Laima", "Jelena", "Mela", "Nadya", "Svetlana", "Elga",
        "Zoraya", "Darinka", "Tasha", "Zlata", "Aleksandra", "Irina", "Katerina", "Natalia", "Oksana", "Valentina",
        "Valeriya", "Kalina", "Yaroslava", "Agata", "Galina", "Nina"
    };

    public string[] MaleLastNames = new string[]
    {
        "Melnikov", "Kuzencov", "Abramowicz", "Abramsky", "Aganin", "Aronov", "Erlić", "Dudko", "Bachinsky", "Bobek",
        "Černý", "Chernik", "Marowitz", "Didak", "Lukashov", "Lukić", "Galowych", "Gavrilov", "Asimov", "Alyokhin",
        "Belyak", "Borzov", "Bosov", "Bovin", "Bulkin", "Besov", "Bespalov", "Chagin", "Chanov", "Demyanov",
        "Denezhkin", "Denisov", "Eldarov", "Eliseev", "Emelin", "Fomin", "Fominykh", "Gruznov", "Gryaznov", "Gubanov",
        "Kadyrov", "Kakhovsky", "Karazin", "Karelin", "Koshkin", "Kosilov", "Lepin", "Lepyokhin", "Malakhov", "Maleev"
    };

    public string[] FemaleLastNames = new string[]
    {
        "Melnikova", "Kuzencova", "Abramowicz", "Abramsky", "Aganina", "Aronova", "Erlić", "Dudko", "Bachinsky",
        "Bobek", "Černý", "Chernik", "Marowitz", "Didak", "Lukashov", "Lukić", "Galowych", "Gavrilova", "Asimova",
        "Alyokhina", "Belyak", "Borzova", "Bosova", "Bovina", "Bulkina", "Besova", "Bespalova", "Chagina", "Chanova",
        "Demyanova", "Denezhkina", "Denisova", "Eldarova", "Eliseeva", "Emelina", "Fomin", "Gruznova", "Gryaznova",
        "Gubanova", "Kadyrova", "Kakhovskaya", "Karazina", "Karelina", "Koshkina", "Kosilova", "Lepina", "Lepyokhina",
        "Malakhova", "Maleeva"
    };


    private void Awake()
    {
        print(GetFullText());
        for (int i = 0; i < 10; i++)
        {
            Sign temp = Instantiate(ObeliskPrefab, new Vector3(0, 1.14f,(5*6)-( i * 6f)), new Quaternion(0,20,0,0))
                .GetComponent<Sign>();
            ObeliskInstances.Add(temp);
            temp.SignText = GetFullText();
        }

        player = Player.Instance;
        if (!player)
        {
            player = FindObjectOfType<Player>();
        }
    }


    private void Update()
    {
        if (player && player.transform.position.z >
            ObeliskInstances[ObeliskInstances.Count / 2].transform.position.z + 1)
        {
            SetPositions(ObeliskInstances[ObeliskInstances.Count / 2].transform.position, 1);
        }
        else if (player && player.transform.position.z <
                 ObeliskInstances[ObeliskInstances.Count / 2].transform.position.z - 1)
        {
            SetPositions(ObeliskInstances[ObeliskInstances.Count / 2].transform.position, -1);
        }
    }


    void SetPositions(Vector3 Center, float multiplier)
    {
        ObeliskInstances[0].transform.position = Center + new Vector3(0, 0, 5 * 6f * multiplier);
        ObeliskInstances[0].SignText = GetFullText();
        ObeliskInstances.Add(ObeliskInstances[0]);
        ObeliskInstances.RemoveAt(0);
    }

    private string GetFullText()
    {
        string fullText = "";
        int gender = Random.Range(0, 2);
        print(gender);

        if (gender == 0)
        {
            fullText = MaleFirstNames[Random.Range(0, MaleFirstNames.Length)] + " " +
                       MaleLastNames[Random.Range(0, MaleLastNames.Length)];
        }
        else
        {
            fullText = FemaleFirstNames[Random.Range(0, FemaleFirstNames.Length)] + " " +
                       FemaleLastNames[Random.Range(0, FemaleLastNames.Length)];
        }

        fullText += "\n" + GetYear().ToString() + "-" + "1346";

        return fullText;
    }


    private int GetYear()
    {
        int year = Random.Range(1280, 1340);


        // make it less likely that children appear
        if (year > 1330)
        {
            year = Random.Range(1280, 1340);
        }


        return year;
    }
}
