# TechTestKetchapp
Test technique pour ketchapp



Bonjour, 

Pour ce test technique, j'ai du reproduire le jeu "Car vs Cops" tout en ajouttant une mécanique de gameplay originale

La vidéo et le build sont aussi dans le git (en temps normal, je ne mettrais pas un build dans le versioning, mais là, ce n'est pas un vrai projet)


La mécanique que j'ai choisi d'ajoutter est la suivante : 
J'ai remarqué que, dans le jeu, ramasser des pieces n'influe pas sur le gameplay, uniquement sur le compteur de piece. 
De plus, ramasser une pièce peu, dans certaines situations, être assez risqué, ce qui fait qu'on va parfois choisir de ne pas se diriger vers une pièce qui est mal placé.

J'ai donc décidé de donner un léger boost de vitesse au joueur lorsqu'il ramasse une piece. 
Ce boost ne dur pas longtemps (2s) et, en contrepartie, j'ai legerement augmenté la vitesse des voitures de police
Cet ajout donne plus envie au joueur de ramasser les pieces, au cas ou le joueur serais uniquement intéréssé par faire du score, et pas par le débloquage de nouveaux véhicules.



Pour faire ce test, j'ai mis environ 5h pour préparer le systeme d'inputs pour en faire un qui soit compatible PC et mibile (C'est réutilisable, c'est pour ça que je le note à part)
Pour le jeu en lui même, j'estime avoir passé entre dix et douze heures dessus.
J'estime ce temps raisonable, sachant que dans un contexte réel, sur une prod de deux semaines, il y aurais du temps pour trouver un concept, du temps d'itération, et du temps de polish.



Il y a certaines features que je n'ai pas eu le temps d'implementer : 
- Les publicitées au lancement du jeu et à la fin de certaines parties (je suppose qu'il y a du code réutilisable pour la plupart de vos jeux sur ce sujet)
- Le lien vers le google store
- le menu des options
- Le sol réfléchissant
- Les contrôles alternatifs
- La séléction des voitures, et la dépense des pièces pour en acheter
- Le systeme de combo de score quand un policier est proche du joueur
- Les lens flare sur les voitures de police
- Les voitures de police alternative (quand j'ai joué, j'ai eu un fourgon, mais je ne sais pas si il y en a d'autres, je ne suis pas assez bon au jeu ^^)


Niveau optimisation, sur mon telephone (Huawei P20) le jeu tourne à ~100fps, sur le téléphone un peu plus vieux d'une amie, il tourne bien (mais je ne connais pas le fps exacte)
Comme je n'ai pas d'autres devices sous la main, je ne peu pas dire, mais si le jeu est trop gourmand, le premier truc que je changerais serais les particules, qui sont des mesh (j'ai copié le jeu de base pour ça).


Personellement, j'ai bien aimé réaliser ce jeu, je pense que l'ultra casual peu être intéréssant à developper, car on change souvent de gameplay, ce qui permet de ne pas tomber dans la routine (mais peut être que je me fourvoi et que la réalité est différente)

si vous avez des question, vous pouvez me contacter par mail "enkilonde@gmail.com"

Merci à vous si vous avez lu jusqu'ici
Bonne journée à vous, 
Enki Londe