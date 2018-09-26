﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PajaroZombie : MonoBehaviour {
    [SerializeField] ParticleSystem prefabExplosion;
    [SerializeField] Text marcadorPuntos;
    [SerializeField] float fuerza = 10f;
    private Rigidbody rb;
    private int puntos = 0;

	void Start ()
    {
        GameConfig.ArrancaJuego();
        rb = GetComponent<Rigidbody>();
        ActualizarMarcador();
    }

    void Update () {
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(transform.up * fuerza);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        puntos++;
        ActualizarMarcador();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //DETENER EL JUEGO
        GameConfig.ParaJuego();

        //CREAR EL SISTEMA DE PARTICULAS
        Instantiate(prefabExplosion, transform.position, Quaternion.identity);

        //DESACTIVAR EL RENDERER
        gameObject.SetActive(false);

        //LLAMAR A FINALIZAR PARTIDA (TRAS 3.5 secs.)
        Invoke("FinalizarPartida", 3.5f);
    }

    private void FinalizarPartida()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);
    }

    private void ActualizarMarcador()
    {
        marcadorPuntos.text = "Score:" + puntos.ToString();
    }

}
