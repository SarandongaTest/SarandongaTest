using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurations : MonoBehaviour {

    public static string temp = "{\"whiteCards\":[{\"text\":\"Coat hanger abortions.\"},{\"text\":\"Man meat.\"},{\"text\":\"Autocannibalism.\"},{\"text\":\"Vigorous jazz hands.\"},{\"text\":\"Flightless birds.\"},{\"text\":\"Pictures of boobs.\"},{\"text\":\"Doing the right thing.\"},{\"text\":\"The violation of our most basic human rights.\"},{\"text\":\"Viagra&reg;.\"},{\"text\":\"Self-loathing.\"},{\"text\":\"Spectacular abs.\"},{\"text\":\"A balanced breakfast.\"},{\"text\":\"Roofies.\"},{\"text\":\"Concealing a boner.\"},{\"text\":\"Amputees.\"},{\"text\":\"The Big Bang.\"},{\"text\":\"Former President George W. Bush.\"},{\"text\":\"The Rev. Dr. Martin Luther King, Jr.\"},{\"text\":\"Smegma.\"},{\"text\":\"Being marginalized.\"},{\"text\":\"Cuddling.\"},{\"text\":\"Laying an egg.\"},{\"text\":\"The Pope.\"},{\"text\":\"Aaron Burr.\"},{\"text\":\"Genital piercings.\"},{\"text\":\"Fingering.\"},{\"text\":\"A bleached asshole.\"},{\"text\":\"Horse meat.\"},{\"text\":\"Fear itself.\"},{\"text\":\"Science.\"},{\"text\":\"Elderly Japanese men.\"},{\"text\":\"Stranger danger.\"},{\"text\":\"The terrorists.\"},{\"text\":\"Praying the gay away.\"},{\"text\":\"Same-sex ice dancing.\"},{\"text\":\"Ethnic cleansing.\"}],\"blackCards\":[{\"text\":\"Why can't I sleep at night?\",\"pick\":1},{\"text\":\"I got 99 problems but _ ain't one.\",\"pick\":1},{\"text\":\"What's a girl's best friend?\",\"pick\":1}]}";

    public static Configurations instance;
    public GameObject whiteCardPrefab;
    public GameObject blackCardPrefab;
    public GameObject DeckSelectorPrefab;
    public GameObject lobbyPlayerPrefab;
    public static Deck deck;
    public static int players;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}
