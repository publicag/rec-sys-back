import sys

import tensorflow as tf
import numpy as np


class Predictor:
    model = tf.keras.models.load_model("D:\MGR\Database\model4")

    def predict(self, input_array):
        new_values = np.asarray(input_array)
        new_values = new_values.reshape(new_values.shape[0], new_values.shape[1], 1)
        prediction = self.model.predict(new_values)
        prediction_value = prediction.argmax(axis=-1)
        return prediction_value


if __name__ == '__main__':
    model = tf.keras.models.load_model("D:\MGR\Database\model4")
    arguments = [[float(x) for x in sys.argv[1:]]]
    new_values = np.asarray(arguments)
    new_values = new_values.reshape(new_values.shape[0], new_values.shape[1], 1)
    prediction = model.predict(new_values)
    prediction_value = prediction.argmax(axis=-1)
    print(prediction_value)
