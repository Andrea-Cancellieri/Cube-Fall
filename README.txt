Andrea Cancellieri
Master in Game Development
Mobile Development


README.txt

Aprendo il file compresso non memorizza i build settings
Durante lo sviluppo ho settato Android Build e Aspect Ratio a 18:9 Portrait

Meccaniche di base:

- Il gioco prende ispirazione da "Missile Command" (1980, Atari).

- La modalità di aspect ratio è esclusivamente in portrait mode.

- L'obiettivo del gioco è quello sopravvivere il più possibile all'attacco di oggetti (principalmente cubi ) che spawnano in alto

sullo schermo e che cadono nella parte bassa di esso, colpendo il suolo, che chiameremo base.

- La base ha una barra della vita, che diminuirà con i colpi dei cubi e che, se azzerata, porterà al game over.

- Unico modo per distruggere le minacce è con una postazione di "fuoco" sulla base, 
dalla quale con l'input del dito possiamo sparare una palla, che se colpirà i cubi farà loro danno. 
Toccando in vicinanza della postazione, si può fare uno slide col dito verso il punto in cui vogliamo colpire, 
rilasciando il dito la palla verrà lanciata verso il punto desiderato (stile simile a Angry Birds o Peggle). 
Più si punta lontano, più sarà la forza indotta sulla palla. 
Dopo ogni lancio c'è sempre un breve delay prima di poter effettuare il successivo.

- Sia la palla che i cubi sono soggetti alla gravità.

- Il punteggio viene calcolato tramite: tempo di sopravvivenza, cubi distrutti, numero di palle lanciate.

- Aumento di difficoltà col passare del tempo (maggior spawn di nemici, spawn di nuove tipologie di nemici).


Power-Ups:

- Palla più grande (danno maggiore);

- Palla esplosiva (esplode se cliccata col dito dopo il lancio);

- Tri-palla (dopo il lancio, se cliccata col dito si divide in 3 palle che proseguono il tragitto);

- Refill barra della vita (istantaneo appena si ottiene il power-up);

I power-ups possono spawnare casualmente e viaggiano orizzontalmente nella mappa. 
Per ottenere i power-ups bisogna colpirli prima che spariscano dopo un certo tempo. 
Le palle speciali sono attivabili tramite una barra in fondo allo schermo, 
alla pressione di un bottone verrà cambiata la palla attualmente caricata sulla postazione.
Le palle speciali hanno un numero di munizioni che viene ricaricato di una certa quantità col corrispondente power-up.
Se le munizioni di una palla speciale vengono esaurite verrà caricata sulla potazione la palla normale.


Cubi standard:

- Cubo normale;

- Cubo grande (ha più vita del cubo standard e fa più danno alla base);

- Tri-cubo (si divide in tre cubi standard se colpito o dopo un certo delay);

Boss:

- Spawner di cubi (si muove orizzontalmente sulla mappa e continua a spawnare cubi finchè non viene distrutto);

- Boss-cubo (insieme di n cubi uniti, a ogni colpo vengono distrutti solo i cubi colpiti).

A inizio partita spawnano solo i cubi standard, col tempo iniziano a spawnare anche gli altri, con un rate che continua ad aumentare.

Puntare a randomicità di ogni partita unito a bilanciamento dei vari spawn di nemici/power-ups.
