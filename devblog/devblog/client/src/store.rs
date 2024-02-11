use serde::{Deserialize, Serialize};
use yewdux::prelude::*;

#[derive(Clone, Serialize, Deserialize, Store, PartialEq, Debug, Default)]
#[store(storage = "local")]
pub struct Store {
    pub username: String,
    pub token: String,
    pub expiration: String,
}
