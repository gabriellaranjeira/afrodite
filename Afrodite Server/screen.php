<?php
date_default_timezone_set('America/Sao_Paulo');
$ip = $_SERVER['REMOTE_ADDR'];
$date = date('Y-m-d H:i:s');
$conn = new PDO('mysql:host=mysql.hostinger.com.br;dbname=u872761257_db', 'u872761257_db', 'lucas3302');
$stmt = $conn->prepare("SELECT nome FROM info WHERE ip='$ip'");
$stmt->execute();
$fetch = $stmt->fetch();
$nome = $fetch['nome'];
$foto = "[".$date."].jpg";
$file = "screenshot/".$foto;
move_uploaded_file($_FILES["file"]["tmp_name"], $file);
$stmt = $conn->prepare("INSERT INTO screen(nome_user, nome_foto) VALUES ('$nome', '$foto')");
$stmt->execute();
?>