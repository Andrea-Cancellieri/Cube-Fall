Andrea Cancellieri
Master in Game Development
Mobile Development


README.txt

Aprendo il file compresso non memorizza i build settings
Durante lo sviluppo ho settato Android Build e Aspect Ratio a 18:9 Portrait

Meccaniche di base:

- Il gioco prende ispirazione da "Missile Command" (1980, Atari).

- La modalit� di aspect ratio � esclusivamente in portrait mode.

- L'obiettivo del gioco � quello sopravvivere il pi� possibile all'attacco di oggetti (principalmente cubi ) che spawnano in alto

sullo schermo e che cadono nella parte bassa di esso, colpendo il suolo, che chiameremo base.

- La base ha una barra della vita, che diminuir� con i colpi dei cubi e che, se azzerata, porter� al game over.

- Unico modo per distruggere le minacce � con una postazione di "fuoco" sulla base, 
dalla quale con l'input del dito possiamo sparare una palla, che se colpir� i cubi far� loro danno. 
Toccando in vicinanza della postazione, si pu� fare uno slide col dito verso il punto in cui vogliamo colpire, 
rilasciando il dito la palla verr� lanciata verso il punto desiderato (stile simile a Angry Birds o Peggle). 
Pi� si punta lontano, pi� sar� la forza indotta sulla palla. 
Dopo ogni lancio c'� sempre un breve delay prima di poter effettuare il successivo.

- Sia la palla che i cubi sono soggetti alla gravit�.

- Il punteggio viene calcolato tramite: tempo di sopravvivenza, cubi distrutti, numero di palle lanciate.

- Aumento di difficolt� col passare del tempo (maggior spawn di nemici, spawn di nuove tipologie di nemici).


Power-Ups:

- Palla pi� grande (danno maggiore);

- Palla esplosiva (esplode se cliccata col dito dopo il lancio);

- Tri-palla (dopo il lancio, se cliccata col dito si divide in 3 palle che proseguono il tragitto);

- Refill barra della vita (istantaneo appena si ottiene il power-up);

I power-ups possono spawnare casualmente e viaggiano orizzontalmente nella mappa. 
Per ottenere i power-ups bisogna colpirli prima che spariscano dopo un certo tempo. 
Le palle speciali sono attivabili tramite una barra in fondo allo schermo, 
alla pressione di un bottone verr� cambiata la palla attualmente caricata sulla postazione.
Le palle speciali hanno un numero di munizioni che viene ricaricato di una certa quantit� col corrispondente power-up.
Se le munizioni di una palla speciale vengono esaurite verr� caricata sulla potazione la palla normale.


Cubi standard:

- Cubo normale;

- Cubo grande (ha pi� vita del cubo standard e fa pi� danno alla base);

- Tri-cubo (si divide in tre cubi standard se colpito o dopo un certo delay);

Boss:

- Spawner di cubi (si muove orizzontalmente sulla mappa e continua a spawnare cubi finch� non viene distrutto);

- Boss-cubo (insieme di n cubi uniti, a ogni colpo vengono distrutti solo i cubi colpiti).

A inizio partita spawnano solo i cubi standard, col tempo iniziano a spawnare anche gli altri, con un rate che continua ad aumentare.

Puntare a randomicit� di ogni partita unito a bilanciamento dei vari spawn di nemici/power-ups.
