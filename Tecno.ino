const int buttonPin1 = 2;
const int buttonPin2 = 3;
const int buttonPin3 = 4;
const int buttonPin4 = 5;

String bitSequence = "";
String frase = "";
String lastChar = "";

const unsigned long debounceDelay = 50;

void setup() {
    pinMode(buttonPin1, INPUT_PULLUP);
    pinMode(buttonPin2, INPUT_PULLUP);
    pinMode(buttonPin3, INPUT_PULLUP);
    pinMode(buttonPin4, INPUT_PULLUP);

    Serial.begin(9600);
    Serial.println("Esperando bits...");
}

void loop() {
    bool isButtonPressed1 = digitalRead(buttonPin1) == HIGH;
    bool isButtonPressed2 = digitalRead(buttonPin2) == HIGH;
    bool isButtonPressed3 = digitalRead(buttonPin3) == HIGH;
    bool isButtonPressed4 = digitalRead(buttonPin4) == HIGH;

    delay(3000); 

    String currentBits = "";
    currentBits += isButtonPressed1 ? '1' : '0';
    currentBits += isButtonPressed2 ? '1' : '0';
    currentBits += isButtonPressed3 ? '1' : '0';
    currentBits += isButtonPressed4 ? '1' : '0';

    if (currentBits == "0000") {
        return; 
    }

    bitSequence += currentBits;

    for (int i = 0; i < currentBits.length(); i++) {
        Serial.print(currentBits[i]);
        delay(200);
    }

    Serial.println();

    if (bitSequence.length() >= 8) {
        String asciiBits = bitSequence.substring(0, 8);
        bitSequence = bitSequence.substring(8);

        char asciiChar = strtol(asciiBits.c_str(), nullptr, 2);
        Serial.print("Carácter ASCII: ");
        Serial.println(asciiChar);

        lastChar = asciiChar;
        frase += asciiChar;

        Serial.print("Frase: ");
        Serial.println(frase);
    }

    Serial.println("Esperando bits...");
    delay(500);
}
