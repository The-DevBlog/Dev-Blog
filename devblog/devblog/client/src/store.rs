use serde::{Deserialize, Serialize};
use yewdux::prelude::*;

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
#[store(storage = "local")]
pub struct Store {
    pub username: String,
    pub token: String,
    pub expiration: String,
}

impl Store {
    pub fn add_to_store(&self, dispatch: Dispatch<Store>) {
        dispatch.reduce_mut(move |store| {
            store.username = self.username.clone();
            store.token = self.token.clone();
        });
    }
}
