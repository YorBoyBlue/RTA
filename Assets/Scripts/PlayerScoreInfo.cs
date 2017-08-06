using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreInfo : MonoBehaviour {
	
	[SerializeField] Text m_nameText;
	[SerializeField] Text m_killsText;
	[SerializeField] Text m_deathsText;

	public string Name { get { return m_nameText.text;} set { m_nameText.text = value; }}
	public string Kills { get { return m_killsText.text;} set { m_killsText.text = value; }}
	public string Deaths { get { return m_deathsText.text;} set { m_deathsText.text = value; }}
}
