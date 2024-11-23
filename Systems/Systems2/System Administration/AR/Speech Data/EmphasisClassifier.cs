public class EmphasisClassifier
{
    public static string ClassifyEmphasis(float pitch, float energy, float volume, float speechRate, float[] spectralContent)
    {
        if (speechRate > 3f && energy > 0.6f && volume < 0.4f)
            return "ASSOCIATIVE_SPEAKING";
        else if (speechRate > 3f && energy > 0.7f && volume > 0.5f)
            return "ATTENTIVE_SPEAKING";
        else if (speechRate > 4f && volume < 0.3f)
            return "AUTOMATIC_SPEAKING";
        else if (pitch < 200 && energy < 0.5f && volume < 0.4f)
            return "CASUAL_SPEAKING";
        else if (energy > 0.7f && speechRate > 3f)
            return "COMMUNICATIVE_SPEAKING";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f)
            return "CONCERNED_SPEAKING";
        else if (speechRate > 3f && volume > 0.4f)
            return "CONVERSATION_SPEAKING";
        else if (pitch > 250 && energy > 0.6f && spectralContent[2] > 0.4f)
            return "COUNTERACTIVE_SPEAKING";
        else if (pitch > 250 && energy > 0.5f && volume > 0.5f)
            return "COUNTERINTUITIVE_SPEAKING";
        else if (pitch > 250 && energy > 0.6f && volume > 0.5f)
            return "DEFENSIVE_SPEAKING";
        else if (speechRate > 4f && volume > 0.6f)
            return "DIRECT_AND_FOCUSED_SPEAKING";
        else if (speechRate > 3f && spectralContent[1] > 0.5f)
            return "DIRECT_AND_POINTED_SPEAKING";
        else if (speechRate > 4f && spectralContent[1] > 0.4f && volume > 0.4f)
            return "DIRECT_AND_FOCUSED_INTENT";
        else if (pitch > 250 && speechRate > 3f)
            return "DIRECT_SPEAKING";
        else if (pitch > 300 && energy > 0.8f && volume > 0.5f)
            return "EMOTIONAL_SPEAKING";
        else if (pitch > 250 && energy > 0.7f && volume > 0.5f)
            return "EMPATHIC_SPEAKING";
        else if (pitch > 250 && energy > 0.8f && volume > 0.6f)
            return "FORCED_SPEAKING";
        else if (pitch < 200 && energy < 0.5f && volume < 0.4f)
            return "FORMAL_SPEAKING";
        else if (pitch < 200 && energy < 0.4f && volume < 0.4f)
            return "GENTLE_SPEAKING";
        else if (pitch > 300 && energy > 0.7f && spectralContent[1] > 0.5f)
            return "HARMONIC_SPEAKING";
        else if (pitch > 300 && speechRate > 3f)
            return "INTELLIGENCE_SPEAKING";
        else if (pitch > 250 && energy > 0.5f && volume > 0.5f)
            return "INTUITIVE_SPEAKING";
        else if (pitch > 250 && energy > 0.6f && volume > 0.4f)
            return "KNOWLEDGEABLE_SPEAKING";
        else if (speechRate > 4f && volume > 0.5f)
            return "LONG_AND_DETAILED_SPEAKING";
        else if (speechRate > 4f && volume > 0.5f)
            return "LONG_AND_DIRECTIVE";
        else if (speechRate > 3f && spectralContent[0] > 0.4f)
            return "LONG_BREATHED";
        else if (speechRate > 5f && energy > 0.8f && volume > 0.6f)
            return "LONG_WINDED";
        else if (volume > 0.7f && spectralContent[2] > 0.5f)
            return "LOUD_AND_BOOMING";
        else if (volume > 0.6f && spectralContent[0] < 0.4f)
            return "LOUD_AND_SOFT_SPOKEN";
        else if (pitch < 200 && energy < 0.4f)
            return "MANUAL_SPEAKING";
        else if (pitch > 300 && spectralContent[1] > 0.5f)
            return "MELODIC_SPEAKING";
        else if (pitch < 200 && energy < 0.5f)
            return "MODEST_SPEAKING";
        else if (pitch > 300 && speechRate > 3f)
            return "MOTIVATED_SPEAKING";
        else if (pitch < 200 && energy < 0.5f && volume < 0.4f)
            return "NEUTRAL_SPEAKING";
        else if (speechRate > 3f && volume > 0.4f)
            return "PASSIVE_SPEAKING";
        else if (speechRate > 3f && volume < 0.4f)
            return "PERIODIC_SPEECH";
        else if (pitch > 300 && spectralContent[2] > 0.5f)
            return "PHILOSOPHICAL_SPEAKING";
        else if (pitch > 250 && energy > 0.6f)
            return "POSITIONED_SPEAKING";
        else if (pitch > 250 && speechRate > 3f)
            return "POSITIVE_SPEAKING";
        else if (pitch > 250 && spectralContent[1] > 0.4f)
            return "PRECISE_AND_TO_THE_POINT";
        else if (speechRate > 4f && spectralContent[2] > 0.5f)
            return "PRESSURED_SPEAKING";
        else if (speechRate > 3f && volume < 0.4f)
            return "PROPER_SPEAKING";
        else if (pitch < 200 && volume < 0.3f)
            return "QUIET_AND_SOFT_SPOKEN_SPEAKING";
        else if (pitch < 200 && volume < 0.4f)
            return "SEMI_MANUAL_SPEAKING";
        else if (pitch > 250 && volume < 0.4f)
            return "SEMI_AUTOMATIC_SPEAKING";
        else if (pitch > 250 && energy > 0.7f)
            return "SERIOUS_SPEAKING";
        else if (pitch > 300 && spectralContent[2] > 0.5f)
            return "SHARP_PITCHED_SPEAKING";
        else if (pitch < 200 && volume < 0.4f)
            return "SHORT_AND_DIRECT";
        else if (speechRate > 3f && volume > 0.5f)
            return "SHORT_BURST";
        else if (pitch < 200 && volume < 0.4f)
            return "SHORT_WINDED";
        else if (volume < 0.3f)
            return "SOFT_SPOKEN";
        else if (volume < 0.3f)
            return "SOFT_TONED";
        else if (pitch < 200 && spectralContent[0] > 0.4f)
            return "SYMPATHIC_SPEAKING";
        else if (speechRate > 4f && volume > 0.5f)
            return "VOCAL_NEGATE";
        else if (pitch > 250 && speechRate > 3f)
            return "WILLED_SPEAKING";
        else if (pitch > 300 && speechRate > 3f)
            return "WISE_SPEAKING";

        return "UNKNOWN";
    }
}
