# K2Romanizer

'가처분' 같이 직관적으로 영어로 바꾸기 힘든 식별자를 한글 로마자 표기법에 따라 변환하는 도구입니다.


## 사용방법
k2r [케이싱] [변환할 한글]
ex) k2r p 가처분

[단위테스트](https://github.com/cplkimth/K2Romanizer/blob/master/K2RomanizerTests/RomanizerTests.cs)를 참고하세요.

## 지원하는 케이싱
### 파스칼 (p)
가처분 -> GaCheoBun
### 카멜 (c)
가처분 -> gaCheoBun
### 스네이크 (s)
가처분 -> ga_cheo_bun
### 대문자 (u)
가처분 -> GACHEOBUN
### 소문자 (l)
가처분 -> gacheobun
### 고유명사 (n)
가처분 -> Gacheobun

## 변환할 문자열을 클립보드로 복사
리눅스에서 문자열을 클립보드로 복사하기 위해서는 xsel 모듈이 설치되어 있어야 합니다.
`sudo apt-get install xsel`